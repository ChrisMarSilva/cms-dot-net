using CMS_WebAPI_OAuth.Contract.Request;
using CMS_WebAPI_OAuth.Contract.Response;
using CMS_WebAPI_OAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CMS_WebAPI_OAuth.Controllers.v1;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(ILogger<AuthController> logger, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _logger = logger;
        _configuration = configuration;
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
                _logger.LogWarning("Usuário já existe: {userName}.", request.UserName);
                return BadRequest("Usuário já existe.");
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
                    _logger.LogWarning("Error ao criar usuário: {message}.", error.Description);
                }
                return BadRequest($"Error ao criar usuário: {create.Errors.First().Description}.");
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
            _logger.LogCritical(e, "Falha crítica, segue a descrição: {description}.", e.Message);
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

            if (listAllUsers.Count() > 0)
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
            _logger.LogCritical(e, "Falha crítica, segue a descrição: {description}.", e.Message);
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
                _logger.LogWarning("Usuário inválido: {userName}.", request.UserName);
                return BadRequest("Usuário inválido.");
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
            _logger.LogCritical(e, "Falha crítica, segue a descrição: {description}.", e.Message);
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
                _logger.LogWarning("Usuário inválido: {userName}.", request.UserName);
                return BadRequest("Usuário inválido.");
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
            _logger.LogCritical(e, "Falha crítica, segue a descrição: {description}.", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Falha interna durante o processamente. Favor tentar novamente");
        }
    }

    [HttpPost("token")]
    //[Consumes("application/x-www-form-urlencoded")] // FromForm
    public async Task<ActionResult<TokenResponseDto>> GenerateToken([FromBody] TokenRequestDto request)
    {
        try
        {
            var applicationUser = await _userManager.FindByNameAsync(request.client_id);
            if (applicationUser is null)
            {
                _logger.LogWarning("Usuário inválido: {userName}.", request.client_id);
                return BadRequest("Usuário inválido.");
            }

            if (await _userManager.IsLockedOutAsync(applicationUser))
            {
                _logger.LogWarning("Usuário bloqueado: {userName}.", request.client_id);
                return BadRequest("Usuário bloqueado.");
            }

            var login = await _signInManager.CheckPasswordSignInAsync(applicationUser, request.client_secret, false);
            //var login = await _signInManager.PasswordSignInAsync(applicationUser, request.client_secret, false, false);

            if (login is null || !login.Succeeded)
            {
                _logger.LogWarning("Credenciais inválidas {userName}.", request.client_id);
                return Unauthorized("Credenciais inválidas.");
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
                _logger.LogWarning("Scope inválido {userName}.", request.client_id);
                return BadRequest("Scope inválido.");
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
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
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

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Secret")!));
            var tokenExpire = DateTime.UtcNow.AddSeconds(_configuration.GetValue<int>("Jwt:Expire"));

            var token = new JwtSecurityTokenHandler()
                .WriteToken(new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims: claims, //new ClaimsIdentity(claims),
                expires: tokenExpire,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512)
            ));

            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refreshToken = Convert.ToBase64String(randomNumber); // Convert.ToBase64String(RandomNumberGenerator.GetBytes(64))

            //user.RefreshToken = refreshToken;
            //user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            //await context.SaveChangesAsync();

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
            _logger.LogCritical(e, "Falha crítica, segue a descrição: {description}.", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Falha interna durante o processamente. Favor tentar novamente");
        }
    }

    [HttpGet("authorize-only")] [Authorize] public IActionResult AuthorizeOnly() => Ok("You are and authorize!");
    [HttpGet("full-only")] [Authorize(Roles = "full")] public IActionResult FullOnly() => Ok("You are and full!");
    [HttpGet("admin-only")] [Authorize(Roles = "Admin")] public IActionResult AdminOnly() => Ok("You are and admin!");
}
