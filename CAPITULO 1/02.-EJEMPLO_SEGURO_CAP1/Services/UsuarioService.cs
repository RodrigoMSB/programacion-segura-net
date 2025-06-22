// ================================================================
// UsuarioService.cs — Implementación del servicio de usuario
// ================================================================
// Define la lógica de negocio para gestionar usuarios, incluyendo
// registro con hash de contraseña y validación de existencia.
// Se basa en principios de arquitectura limpia y seguridad aplicada.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS
// ---------------------------------------------------------------

// Importa el contexto de base de datos que expone los DbSet y gestiona transacciones.
using EjemploSeguridadCapitulo1.Data;

// Importa el modelo de dominio Usuario para instanciar y guardar entidades.
using EjemploSeguridadCapitulo1.Models;

// Librería de criptografía para generar hash seguro de la contraseña.
using System.Security.Cryptography;

// Librería para manipulación de cadenas de texto y conversión a bytes.
using System.Text;

namespace EjemploSeguridadCapitulo1.Services
{
    /// <summary>
    /// Implementación concreta de IUsuarioService.
    /// Responsable de:
    /// - Verificar existencia de usuarios.
    /// - Registrar nuevos usuarios con hash de contraseña.
    /// 
    /// Principios aplicados:
    /// - Separación de responsabilidades.
    /// - Persistencia a través de EF Core.
    /// - Buenas prácticas de seguridad para almacenamiento de credenciales.
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        /// <summary>
        /// Contexto de base de datos inyectado por dependencia.
        /// Permite acceder a tablas, realizar consultas y guardar cambios.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que recibe el contexto para persistencia.
        /// </summary>
        /// <param name="context">Instancia del ApplicationDbContext inyectado.</param>
        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Verifica si ya existe un usuario con el nombre especificado.
        /// Evita registros duplicados y se usa en validaciones de negocio.
        /// </summary>
        /// <param name="username">Nombre de usuario a comprobar.</param>
        /// <returns>True si el usuario existe; False en caso contrario.</returns>
        public bool UsuarioExiste(string username)
        {
            return _context.Usuarios.Any(u => u.Username == username);
        }

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// Aplica hash de la contraseña usando SHA256.
        /// 
        /// Nota: para mayor seguridad, se recomienda PBKDF2 o bcrypt en escenarios reales.
        /// </summary>
        /// <param name="username">Nombre de usuario único.</param>
        /// <param name="email">Correo electrónico válido del usuario.</param>
        /// <param name="passwordPlano">Contraseña en texto plano; se convierte a hash.</param>
        public void CrearUsuario(string username, string email, string passwordPlano)
        {
            // Generar hash de la contraseña usando SHA256 de forma rápida.
            string hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(passwordPlano)));

            // Crear nueva instancia de Usuario con datos validados.
            var nuevoUsuario = new Usuario
            {
                Username = username,
                Email = email,
                PasswordHash = hash,
                Rol = "Usuario" // Asignación de rol por defecto.
            };

            // Agregar y persistir en la base de datos.
            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();
        }
    }
}