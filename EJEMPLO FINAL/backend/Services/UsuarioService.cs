// ===============================================================================================
// UsuarioService.cs — Servicio para Gestión de Usuarios
// ===============================================================================================
// Este servicio implementa la lógica de negocio relacionada con:
// - Registro y autenticación de usuarios.
// - Creación de cuentas bancarias asociadas.
// - Consulta de usuarios y sus cuentas.
//
// Encapsula todas las operaciones sobre la entidad Usuario y sus relaciones
// (CuentaBancaria), aplicando criptografía segura para credenciales.
//
// Este diseño sigue el principio de responsabilidad única y desacopla
// la lógica de negocio del controlador o la capa de presentación.
//
// ===============================================================================================

// -----------------------------------------------------------------------------------------------
// IMPORTS — Dependencias esenciales para acceso a datos y operaciones criptográficas.
// -----------------------------------------------------------------------------------------------
using SeguridadBancoFinal.Data;                     // Contexto de base de datos (Entity Framework).
using SeguridadBancoFinal.Models;                   // Entidades del dominio (Usuario, CuentaBancaria).
using Microsoft.EntityFrameworkCore;                // EF Core para consultas con Include y seguimiento.

namespace SeguridadBancoFinal.Services
{
    /// <summary>
    /// Servicio que encapsula toda la lógica relacionada a usuarios del sistema.
    /// Incluye registro seguro, login, consultas y creación de cuentas bancarias.
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;
        private readonly CryptoService _crypto;

        /// <summary>
        /// Constructor con inyección de dependencias.
        /// </summary>
        public UsuarioService(ApplicationDbContext context, CryptoService crypto)
        {
            _context = context;
            _crypto = crypto;
        }

        // =======================================================================================
        // REGISTRAR NUEVO USUARIO
        // =======================================================================================

        /// <summary>
        /// Registra un nuevo usuario, aplicando hash seguro a la contraseña.
        /// </summary>
        public Usuario RegistrarUsuario(string nombre, string email, string passwordPlano, string rol = "Cliente")
        {
            if (_context.Usuarios.Any(u => u.Email == email))
                throw new InvalidOperationException("Email ya registrado.");

            var (hash, salt) = _crypto.HashPassword(passwordPlano);

            var nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Email = email,
                PasswordHash = hash,
                Salt = salt,
                Rol = rol
            };

            _context.Usuarios.Add(nuevoUsuario);
            _context.SaveChanges();

            return nuevoUsuario;
        }

        /// <summary>
        /// Crea un cliente sin contraseña (por ejemplo, creado por un admin).
        /// </summary>
        public Usuario CrearClienteSinPassword(string nombre, string email)
        {
            var usuario = new Usuario
            {
                Nombre = nombre,
                Email = email,
                PasswordHash = "",
                Salt = "",
                Rol = "Cliente"
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        /// <summary>
        /// Asigna una contraseña a un cliente previamente creado sin ella.
        /// </summary>
        public void CompletarRegistroCliente(string email, string password)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);

            if (usuario == null)
                throw new Exception("Usuario no encontrado.");

            if (!string.IsNullOrEmpty(usuario.PasswordHash))
                throw new Exception("Este usuario ya tiene contraseña asignada.");

            var (hash, salt) = _crypto.HashPassword(password);
            usuario.PasswordHash = hash;
            usuario.Salt = salt;

            _context.SaveChanges();
        }

        /// <summary>
        /// Variante redundante de CompletarRegistroCliente.
        /// ❗ Se sugiere mantener solo una versión.
        /// </summary>
        public Usuario CompletarRegistro(string email, string passwordPlano)
        {
            var usuario = _context.Usuarios.SingleOrDefault(u => u.Email == email);

            if (usuario == null)
                throw new Exception("Usuario no encontrado.");

            if (!string.IsNullOrEmpty(usuario.PasswordHash))
                throw new Exception("La cuenta ya tiene contraseña asignada.");

            var (hash, salt) = _crypto.HashPassword(passwordPlano);
            usuario.PasswordHash = hash;
            usuario.Salt = salt;

            _context.SaveChanges();
            return usuario;
        }

        // 💡 RECOMENDACIÓN: Unificar `CompletarRegistroCliente` y `CompletarRegistro`.

        // =======================================================================================
        // VALIDAR LOGIN
        // =======================================================================================

        /// <summary>
        /// Valida si el email y contraseña coinciden con un usuario registrado.
        /// </summary>
        public Usuario? ValidarCredenciales(string email, string passwordPlano)
        {
            var usuario = _context.Usuarios
                                  .FirstOrDefault(u => u.Email == email);

            if (usuario == null)
                return null;

            if (string.IsNullOrEmpty(usuario.PasswordHash) || string.IsNullOrEmpty(usuario.Salt))
                return null;

            bool valido = _crypto.VerifyPassword(passwordPlano, usuario.Salt, usuario.PasswordHash);
            return valido ? usuario : null;
        }

        // =======================================================================================
        // CUENTAS
        // =======================================================================================

        /// <summary>
        /// Crea una cuenta bancaria para un usuario dado.
        /// </summary>
        public CuentaBancaria CrearCuentaParaUsuario(int usuarioId, string numeroCuenta, decimal saldoInicial)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
            if (usuario == null)
                throw new Exception("Usuario no encontrado.");

            var cuenta = new CuentaBancaria
            {
                NumeroCuenta = numeroCuenta,
                Saldo = saldoInicial,
                UsuarioId = usuarioId
            };

            _context.CuentasBancarias.Add(cuenta);
            _context.SaveChanges();

            return cuenta;
        }

        // =======================================================================================
        // CONSULTAS
        // =======================================================================================

        /// <summary>
        /// Busca un usuario por su ID, incluyendo sus cuentas asociadas.
        /// </summary>
        public Usuario? ObtenerPorId(int id)
        {
            return _context.Usuarios
                           .Include(u => u.Cuentas)
                           .FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Busca un usuario por email (sin tracking).
        /// </summary>
        public Usuario? ObtenerPorEmail(string email)
        {
            return _context.Usuarios
                           .AsNoTracking()
                           .Include(u => u.Cuentas)
                           .FirstOrDefault(u => u.Email == email);
        }

        /// <summary>
        /// Retorna todos los usuarios con sus cuentas (para uso administrativo).
        /// </summary>
        public IEnumerable<Usuario> ObtenerTodos()
        {
            return _context.Usuarios
                           .Include(u => u.Cuentas)
                           .AsNoTracking()
                           .ToList();
        }

        /// <summary>
        /// Retorna una cuenta por número exacto.
        /// </summary>
        public CuentaBancaria? ObtenerCuentaPorNumero(string numeroCuenta)
        {
            return _context.CuentasBancarias
                           .FirstOrDefault(c => c.NumeroCuenta == numeroCuenta);
        }
    }
}