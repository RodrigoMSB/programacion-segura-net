// =====================================================================================
// AuthService.cs — Servicio de Autenticación JWT
// =====================================================================================
// Este servicio es responsable de generar tokens JWT seguros para usuarios autenticados.
// Utiliza claves simétricas y HMAC-SHA256 como método de firma.
//
// JWT (JSON Web Token) es un estándar (RFC 7519) que permite transmitir información de
// autenticación y autorización entre partes de forma segura, compacta y auto-contenida.
//
// -------------------------------------------------------------------------------------
// FUNCIONALIDAD:
// - Generar tokens JWT personalizados con información del usuario (claims).
// - Configurable a través del archivo appsettings.json (clave secreta y expiración).
//
// -------------------------------------------------------------------------------------
// SEGURIDAD:
// - Clave secreta leída desde la configuración para evitar exposición directa.
// - El token incluye ID de usuario, nombre, email y rol como claims.
// - Se firma con HMAC SHA256, y puede ser validado sin acceso a base de datos.
//
// =====================================================================================

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SeguridadBancoFinal.Models;
using Microsoft.Extensions.Configuration;

namespace SeguridadBancoFinal.Services
{
    /// <summary>
    /// Servicio responsable de generar tokens JWT para autenticar y autorizar usuarios
    /// dentro del sistema. Utiliza HMAC SHA256 y una clave secreta configurable.
    /// </summary>
    public class AuthService
    {
        private readonly string _jwtSecret;
        private readonly int _jwtLifespanMinutes;

        /// <summary>
        /// Constructor que carga la configuración JWT desde appsettings.json.
        /// </summary>
        /// <param name="configuration">Interfaz para acceder a la configuración de la app.</param>
        public AuthService(IConfiguration configuration)
        {
            _jwtSecret = configuration["JwtSettings:Secret"] 
                         ?? throw new ArgumentNullException("Jwt Secret not configured.");

            _jwtLifespanMinutes = int.Parse(configuration["JwtSettings:LifespanMinutes"] ?? "60");
        }

        // =======================================================================
        // MÉTODO: GenerateToken
        // =======================================================================

        /// <summary>
        /// Genera un JWT firmado para el usuario autenticado.
        /// </summary>
        /// <param name="usuario">Entidad Usuario autenticada.</param>
        /// <returns>Token JWT firmado como string.</returns>
        public string GenerateToken(Usuario usuario)
        {
            // =======================
            // Claims (información útil dentro del token)
            // =======================
            var claims = new[]
            {
                // Identificador del usuario
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),

                // Nombre completo (recomendado por ClaimTypes)
                new Claim(ClaimTypes.Name, usuario.Nombre),

                // Email del usuario (también para validaciones)
                new Claim(ClaimTypes.Email, usuario.Email),

                // Rol del usuario (para autorización por perfil)
                new Claim(ClaimTypes.Role, usuario.Rol),

                // Identificador único del token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // =======================
            // Firma del token
            // =======================
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // =======================
            // Construcción del JWT
            // =======================
            var token = new JwtSecurityToken(
                issuer: null,                // No se está utilizando un emisor fijo
                audience: null,              // No se está utilizando una audiencia fija
                claims: claims,              // Claims definidos arriba
                expires: DateTime.UtcNow.AddMinutes(_jwtLifespanMinutes), // Tiempo de vida
                signingCredentials: creds    // Firma digital
            );

            // =======================
            // Serialización a string
            // =======================
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}