using CMS_WebAPI_OAuth.Contract.Request;
using CMS_WebAPI_OAuth.Contract.Response;
using CMS_WebAPI_OAuth.Data.Context;
using CMS_WebAPI_OAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CMS_WebAPI_OAuth.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(ILogger<AuthController> logger, IConfiguration configuration, AppDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _logger = logger;
        _configuration = configuration;
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<ActionResult<CreateUserResponseDto>> AddUser([FromBody] CreateUserRequestDto request)
    {
        try
        {
            var applicationUser = await _userManager.FindByNameAsync(request.UserName);
            if (applicationUser is not null)
            {
                _logger.LogWarning("Usu�rio j� existe: {userName}.", request.UserName);
                return BadRequest("Usu�rio j� existe.");
            }

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = true,
                PhoneNumber = request.Telefone
            };

            var create = await _userManager.CreateAsync(user, request.Password);
            if (!create.Succeeded)
            {
                foreach (var error in create.Errors)
                {
                    _logger.LogWarning("Error ao criar usu�rio: {message}.", error.Description);
                }
                return BadRequest($"Error ao criar usu�rio: {create.Errors.First().Description}.");
            }

            var response = new CreateUserResponseDto
            {
                UserName = request.UserName,
                Password = ""
            };

            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Falha cr�tica, segue a descri��o: {description}.", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Falha interna durante o processamente. Favor tentar novamente");
        }
    }

    [HttpGet("users")]
    public async Task<ActionResult<List<UserResponseDto>>> GetUsers()
    {
        try
        {
            var listAllUsers = await _userManager.Users.ToListAsync();
            var response = new List<UserResponseDto>();

            if (listAllUsers.Count > 0)
            {
                foreach (var user in listAllUsers)
                {
                    var scopes = await _userManager.GetRolesAsync(user);

                    response.Add(new UserResponseDto
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Telefone = user.PhoneNumber,
                        Perfil = (await _userManager.GetClaimsAsync(user)).FirstOrDefault()?.Value,
                        Escopos = scopes
                    });
                }
                
                response = response.OrderBy(x => x.UserName).ToList();
            }

            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Falha cr�tica, segue a descri��o: {description}.", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Falha interna durante o processamente. Favor tentar novamente");
        }
    }

    [HttpPost("grant")]
    public async Task<ActionResult<bool>> AddScopeToUser([FromBody] GrantUserRequestDto request)
    {
        try
        {
            var applicationUser = await _userManager.FindByNameAsync(request.UserName);
            if (applicationUser is null)
            {
                _logger.LogWarning("Usu�rio inv�lido: {userName}.", request.UserName);
                return BadRequest("Usu�rio inv�lido.");
            }

            var userRoles = await _userManager.GetRolesAsync(applicationUser);

            var result = await _userManager.RemoveFromRolesAsync(applicationUser, userRoles);

            if (result.Succeeded)
            {
                result = await _userManager.AddToRolesAsync(applicationUser, request.Scopes.Split(',', StringSplitOptions.RemoveEmptyEntries));

                if (result.Succeeded)
                    return Ok(true);
            }

            foreach (var error in result.Errors)
            {
                _logger.LogWarning("Error ao criar scope: {message}.", error.Description);
            }
            return BadRequest($"Error ao criar scope: {result.Errors.First().Description}.");
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Falha cr�tica, segue a descri��o: {description}.", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Falha interna durante o processamente. Favor tentar novamente");
        }
    }
    
    [HttpPost("role")]
    public async Task<ActionResult<bool>> AddRoleToUser([FromBody] RoleUserRequestDto request)
    {
        try
        {
            var applicationUser = await _userManager.FindByNameAsync(request.UserName);
            if (applicationUser is null)
            {
                _logger.LogWarning("Usu�rio inv�lido: {userName}.", request.UserName);
                return BadRequest("Usu�rio inv�lido.");
            }

            var role = request.Role.ToLowerInvariant();
            var claims = await _userManager.GetClaimsAsync(applicationUser);
            IdentityResult result;

            if (claims.Any())
            {
                result = await _userManager.ReplaceClaimAsync(applicationUser, claims.First(), new Claim(ClaimTypes.Role, role));
            }
            else
            {
                result = await _userManager.AddClaimAsync(applicationUser, new Claim(ClaimTypes.Role, role));
            }

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogWarning("Error ao criar scope: {message}.", error.Description);
                }
                return BadRequest($"Error ao criar scope: {result.Errors.First().Description}.");
            }

            return Ok(true);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Falha cr�tica, segue a descri��o: {description}.", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Falha interna durante o processamente. Favor tentar novamente");
        }
    }

    [HttpPost("token")]
    public async Task<ActionResult<TokenResponseDto>> GenerateToken([FromBody] TokenRequestDto request)
    {
        try
        {
            var applicationUser = await _userManager.FindByNameAsync(request.client_id);
            
            if (applicationUser is null)
            {
                _logger.LogWarning("Usu�rio inv�lido: {userName}.", request.client_id);
                return BadRequest("Usu�rio inv�lido.");
            }

            if (await _userManager.IsLockedOutAsync(applicationUser))
            {
                _logger.LogWarning("Usu�rio bloqueado: {userName}.", request.client_id);
                return BadRequest("Usu�rio bloqueado.");
            }

            var login = await _signInManager.CheckPasswordSignInAsync(applicationUser, request.client_secret, false);

            if (login is null || !login.Succeeded)
            {
                _logger.LogWarning("Credenciais inv�lidas {userName}.", request.client_id);
                return Unauthorized("Credenciais inv�lidas.");
            }

            var scopes = request.scope.ToLowerInvariant().Split(',', StringSplitOptions.RemoveEmptyEntries);

            var scopeValidate = true;
            foreach (var s in scopes)
            {
                if (await _userManager.IsInRoleAsync(applicationUser, s)) continue;

                scopeValidate = false;
                break;
            }

            if (!scopeValidate)
            {
                _logger.LogWarning("Scope inv�lido {userName}.", request.client_id);
                return BadRequest("Scope inv�lido.");
            }

            var friendlyName = applicationUser.UserName;
            if (!string.IsNullOrEmpty(applicationUser.FirstName))
            {
                friendlyName = applicationUser.FirstName;
                if (!string.IsNullOrEmpty(applicationUser.LastName))
                    friendlyName += " " + applicationUser.LastName;
            }
           
            var claims = new List<Claim>
            {
                new Claim("user_id", applicationUser.Id.ToString("N")),
                new Claim("display_name", friendlyName ?? "-")
            };

            var claimsUser = await _userManager.GetClaimsAsync(applicationUser);
            if (claimsUser?.Any() ?? false)
            {
                foreach (var item in claimsUser)
                {
                    if (item.Type == ClaimTypes.Role)
                        claims.Add(new Claim("role", item.Value)); // AllowedGrantTypes
                    else
                        claims.Add(item);
                }
            }

            var scopesUser = (await _userManager.GetRolesAsync(applicationUser)).Select(r => r.Normalize().ToLowerInvariant()).ToList();
            foreach (var item in scopesUser)
            {
                claims.Add(new Claim("aud", item)); // AllowedScopes 
            }

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration.GetValue<string>("Jwt:Secret")!));
            var tokenExpire = DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>("Jwt:Expire"));
            
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims: claims,
                expires: tokenExpire,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            applicationUser.SetRefreshToken(refreshToken, tokenExpire);
            await _context.SaveChangesAsync();

            var response = new TokenResponseDto
            {
                expires_in = new DateTimeOffset(tokenExpire).ToUnixTimeSeconds(),
                access_token = token,
                refresh_token = refreshToken,
                token_type = "Bearer",
                scope = string.Join(',', scopesUser),
            };

            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Falha cr�tica, segue a descri��o: {description}.", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Falha interna durante o processamente. Favor tentar novamente");
        }
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<RefreshTokenResponseDto>> GenerateRefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        try
        {
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration.GetValue<string>("Jwt:Secret")!));
            var key = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
                IssuerSigningKey = key.Key
            };
            var handler = new JwtSecurityTokenHandler();
            var principal = handler.ValidateToken(request.AccessToken, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(key.Algorithm, StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.LogWarning("access token/refresh token inv�lido.");
                return BadRequest("access token/refresh token inv�lido.");
            }

            var userId = principal.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value!; // ClaimTypes.Name // principal.Identity!.Name
            var applicationUser = await _userManager.FindByIdAsync(userId); 

            if (applicationUser is null ||
                string.IsNullOrEmpty(applicationUser.RefreshToken) || 
                !applicationUser.RefreshToken.Equals(request.RefreshToken, StringComparison.Ordinal) || 
                applicationUser.DtHrExpireRefreshToken <= DateTime.UtcNow)
            {
                _logger.LogWarning("access token/refresh token inv�lido.");
                return BadRequest("access token/refresh token inv�lido.");
            }

            if (await _userManager.IsLockedOutAsync(applicationUser))
            {
                _logger.LogWarning("Usu�rio bloqueado: {idUser}.", applicationUser.Id.ToString());
                return BadRequest("Usu�rio bloqueado.");
            }

            var scopes = string.Join(',', principal.Claims.Where(x => x.Type == "aud").Select(y => y.Value.Normalize().ToLowerInvariant()));
           
            if (!await UserStillHasScopes(applicationUser, scopes))
            {
                _logger.LogWarning("Usu�rio perdeu algum escopo: {idUser}.", applicationUser.Id.ToString());
                return BadRequest("Usu�rio perdeu algum escopo.");
            }

            var tokenExpire = DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>("Jwt:Expire"));

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims: principal.Claims,
                expires: tokenExpire,
                signingCredentials: key
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            applicationUser.SetRefreshToken(refreshToken, tokenExpire);
            await _context.SaveChangesAsync();

            var response = new RefreshTokenResponseDto
            {
                expires_in = new DateTimeOffset(tokenExpire).ToUnixTimeSeconds(),
                access_token = token,
                refresh_token = refreshToken,
                token_type = "Bearer",
                scope = string.Join(',', principal.Claims.Where(x => x.Type == "aud").Select(y => y.Value)),
            };

            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Falha cr�tica, segue a descri��o: {description}.", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Falha interna durante o processamente. Favor tentar novamente");
        }
    }

    [HttpGet("authorize")] [Authorize]                  public IActionResult AuthorizeOnly() => Ok("You are and authorize!");
    [HttpGet("full")]      [Authorize(Roles = "full")]  public IActionResult FullOnly()      => Ok("You are and full!");
    [HttpGet("admin")]     [Authorize(Roles = "Admin")] public IActionResult AdminOnly()     => Ok("You are and admin!");
    
    private async Task<bool> UserStillHasScopes(ApplicationUser user, string scopes)
    {
        var audience = _configuration.GetValue<string>("Jwt:Audience")!;
        var scopeValidate = true;

        foreach (var scope in scopes.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            if (audience.Equals(scope, StringComparison.OrdinalIgnoreCase)) continue;
            if (await _userManager.IsInRoleAsync(user, scope)) continue;
            scopeValidate = false;
            break;
        }

        return scopeValidate;
    }
}
