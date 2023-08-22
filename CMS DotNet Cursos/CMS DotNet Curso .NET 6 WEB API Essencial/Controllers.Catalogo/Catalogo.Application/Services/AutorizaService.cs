using Catalogo.Application.Dtos;
using Catalogo.Application.Interfaces;
using Catalogo.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Catalogo.Application.Services;

public class AutorizaService : IAutorizaService
{

    private readonly ILogger<ProdutoService> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly string _className;

    public AutorizaService(
        ILogger<ProdutoService> logger,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _className = GetType().FullName;

        _logger.LogInformation($"{_className}");
    }

    public async Task<UsuarioResponseDTO> LoginAsync(LoginRequestDTO request)
    {
        _logger.LogInformation($"{_className}.LoginAsync()");
        try
        {
            // var user = await userManager.FindByEmailAsync(loginRequest.Email);
            // if (user == null) return Results.BadRequest("Email Invalido");
            // var userCheckPassword = await userManager.CheckPasswordAsync(user, loginRequest.Password);
            // if (!userCheckPassword) return Results.BadRequest("Senha Invalida");
            //var subject = new ClaimsIdentity(new Claim[]
            // {
            //            new Claim(ClaimTypes.NameIdentifier, user.Id),
            //            new Claim(ClaimTypes.Email, user.UserName), //  loginRequest.Email
            // });
            //var claims = await userManager.GetClaimsAsync(user);
            //if (claims != null)  subject.AddClaims(claims);
            //var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = subject,
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            //    Audience = configuration["JwtBearerTokenSettings:Audience"],
            //    Issuer = configuration["JwtBearerTokenSettings:Issuer"],
            //    Expires = environment.IsDevelopment() || environment.IsStaging() ? DateTime.UtcNow.AddYears(1) : DateTime.UtcNow.AddMinutes(60),
            //};
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.CreateToken(tokenDescriptor);

            // (HttpContext http)
            // var user = http.User;
            //var idUser = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            //var nameUser = user.Claims.FirstOrDefault(c => c.Type == "Name").Value;
            //var cpfUser = user.Claims.FirstOrDefault(c => c.Type == "Cpf").Value;

            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
                return null; // throw new Exception("Login Inválido"); // return BadRequest("Login Inválido....");

            var response = this.GeraToken(request.Email);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.LoginAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task<UsuarioResponseDTO> RegisterAsync(UsuarioRequestDTO request)
    {
        _logger.LogInformation($"{_className}.RegisterAsync()");
        try
        {
            var user = new ApplicationUser { UserName = request.Email, Email = request.Email, EmailConfirmed = true };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return null; // throw new Exception(result.Errors.ToString()); // BadRequest(result.Errors);

            // var claimsResult = await this._userManager.AddClaimsAsync(newUser, claims);
            // if (!claimsResult.Succeeded)
            // return null;

            await _signInManager.SignInAsync(user, isPersistent: false);

            var response = this.GeraToken(request.Email);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.RegisterAsync(Erro: {ex.Message})");
            throw;
        }
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    private UsuarioResponseDTO GeraToken(string email)
    {
        _logger.LogInformation($"{_className}.GeraToken()");
        try
        {
            var claims = new List<Claim> // new[]
            {
                new Claim("meuValor", "oque voce quiser"),
                new Claim("meuPet", "pipoca"),
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.UniqueName, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // new Claim(ClaimTypes.Email, userInfo.Email),
                // new Claim(ClaimTypes.Role, "Gerente"),
            };

            var key = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credenciais = new SigningCredentials(key: key, algorithm: SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(double.Parse(_configuration["TokenConfiguration:ExpireHours"]));

            JwtSecurityToken tokenHandler = new JwtSecurityToken(
              issuer: _configuration["TokenConfiguration:Issuer"],
              audience: _configuration["TokenConfiguration:Audience"],
              claims: claims,
              expires: expiration,
              signingCredentials: credenciais);

            var response = new UsuarioResponseDTO()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(tokenHandler),
                Expiration = expiration,
                Message = "Token JWT OK"
                // RefreshToken = GenerateRefreshToken(),
                // RefreshTokenExpiration = DateTime.Now.AddDays(7)
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{_className}.GeraToken(Erro: {ex.Message})");
            throw;
        }
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
