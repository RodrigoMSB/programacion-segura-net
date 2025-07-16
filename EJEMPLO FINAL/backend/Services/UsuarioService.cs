using SeguridadBancoFinal.Data;
using SeguridadBancoFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace SeguridadBancoFinal.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;
        private readonly CryptoService _crypto;

        public UsuarioService(ApplicationDbContext context, CryptoService crypto)
        {
            _context = context;
            _crypto = crypto;
        }

        // =======================================
        // REGISTRAR NUEVO USUARIO
        // =======================================
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
        // =======================================
        // VALIDAR CREDENCIALES DE LOGIN
        // =======================================
        public Usuario? ValidarCredenciales(string email, string passwordPlano)
        {
            var usuario = _context.Usuarios
                                  .FirstOrDefault(u => u.Email == email);

            if (usuario == null)
                return null;

            // Verificar que tenga datos válidos
            if (string.IsNullOrEmpty(usuario.PasswordHash) || string.IsNullOrEmpty(usuario.Salt))
                return null;

            bool valido = _crypto.VerifyPassword(passwordPlano, usuario.Salt, usuario.PasswordHash);
            return valido ? usuario : null;
        }

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

        // =======================================
        // OBTENER USUARIO POR ID
        // =======================================
        public Usuario? ObtenerPorId(int id)
        {
            return _context.Usuarios
                           .Include(u => u.Cuentas)
                           .FirstOrDefault(u => u.Id == id);
        }

        // =======================================
        // OBTENER USUARIO POR EMAIL
        // =======================================
        public Usuario? ObtenerPorEmail(string email)
        {
            return _context.Usuarios
                           .Include(u => u.Cuentas)
                           .FirstOrDefault(u => u.Email == email);
        }

        // =======================================
        // LISTAR TODOS LOS USUARIOS (para Admin)
        // =======================================
        public IEnumerable<Usuario> ObtenerTodos()
        {
            return _context.Usuarios
                           .Include(u => u.Cuentas)
                           .AsNoTracking()
                           .ToList();
        }
    }
}