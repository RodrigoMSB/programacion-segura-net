using SeguridadBancoFinal.Data;
using SeguridadBancoFinal.Models;

namespace SeguridadBancoFinal.Services
{
    public class CuentaService : ICuentaService
    {
        private readonly ApplicationDbContext _context;

        public CuentaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CrearCuenta(CuentaBancaria cuenta)
        {
            _context.CuentasBancarias.Add(cuenta);
            _context.SaveChanges();
        }

        public List<CuentaBancaria> ObtenerCuentasPorUsuario(int usuarioId)
        {
            return _context.CuentasBancarias
                           .Where(c => c.UsuarioId == usuarioId)
                           .ToList();
        }
    }
}