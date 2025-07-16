using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SeguridadBancoFinal.Models;
using Microsoft.Extensions.Configuration;

namespace SeguridadBancoFinal.Services
{
    public class AuthService
    {
        private readonly string _jwtSecret;
        private readonly int _jwtLifespanMinutes;

        public AuthService(IConfiguration configuration)
        {
            // Obtener configuración desde appsettings
            _jwtSecret = configuration["JwtSettings:Secret"] ?? throw new ArgumentNullException("Jwt Secret not configured.");
            _jwtLifespanMinutes = int.Parse(configuration["JwtSettings:LifespanMinutes"] ?? "60");
        }

        // ================================
        // Generar JWT para un usuario
        // ================================
        public string GenerateToken(Usuario usuario)
        {
            // Claims que se incluirán en el token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // ID único para el token
            };

            // Clave secreta y firma
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear el token
            var token = new JwtSecurityToken(
                issuer: null,          // Opcional
                audience: null,        // Opcional
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtLifespanMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}