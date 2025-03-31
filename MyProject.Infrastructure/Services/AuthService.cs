using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyProject.Core.Exceptions;
using MyProject.Core.Interfaces;
using MyProject.Core.Models;
using MyProject.Infrastructure.Interfaces;

namespace MyProject.Infrastructure.Services;

public class AuthService(
    IUserRepository userRepository,
    IConfiguration configuration) : IAuthService
{
    private const int JwtDefaultExpireDays = 7;
    private const string JwtConfigurationSectionNotFound = "JwtConfigurationSectionNotFound";
    private const string JwtSecretKeyNotFound = "JWT secret key not found";
    private const string JwtExpireDaysMustBePositive = "ExpireDays must be positive";
    private const string UsernameAlreadyExists = "Username already exists";
    private const string UserCreationFailed = "User creation failed";
    
    public async Task<User> Register(string username, string password)
    {
        if (await userRepository.GetByUsernameAsync(username) != null)
        {
            throw new ApiException(UsernameAlreadyExists);
        }

        CreatePasswordHash(password, out var hash, out var salt);
        
        var user = new User
        {
            Username = username,
            PasswordHash = hash,
            PasswordSalt = salt
        };

        await userRepository.AddAsync(user);
        return user ?? throw new InvalidOperationException(UserCreationFailed);    
    }

    public async Task<string> Login(string username, string password)
    {
        var user = await userRepository.GetByUsernameAsync(username);
        if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            throw new InvalidCredentialsException();
        }

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSection = configuration.GetSection("Jwt");
        if (!jwtSection.Exists())
        {
            throw new JwtConfigurationException(JwtConfigurationSectionNotFound);
        }

        var jwtKey = jwtSection["Key"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new JwtConfigurationException(JwtSecretKeyNotFound);
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
    
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expireDays = jwtSection.GetValue<double?>("ExpireDays") ?? JwtDefaultExpireDays;
        if (expireDays <= 0)
        {
            throw new JwtConfigurationException(JwtExpireDaysMustBePositive);
        }

        var token = new JwtSecurityToken(
            issuer: jwtSection["Issuer"],
            audience: jwtSection["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(expireDays), // Используем UTC время
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        using var hmac = new HMACSHA512(storedSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(storedHash);
    }
}