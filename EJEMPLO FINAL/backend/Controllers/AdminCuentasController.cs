using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeguridadBancoFinal.DTOs;
using SeguridadBancoFinal.Models;
using SeguridadBancoFinal.Data;

namespace SeguridadBancoFinal.Controllers
{
    [ApiController]
    [Route("admin/cuentas")]
    [Authorize(Roles = "Admin")]
    public class AdminCuentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminCuentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // GET: /admin/cuentas
        // ==========================================
        [HttpGet]
        public IActionResult GetTodasLasCuentas()
        {
            var cuentas = _context.CuentasBancarias
                                  .Select(c => new
                                  {
                                      c.Id,
                                      c.NumeroCuenta,
                                      c.Saldo,
                                      c.UsuarioId
                                  })
                                  .ToList();

            return Ok(cuentas);
        }

        // ==========================================
        // POST: /admin/cuentas
        // ==========================================
        [HttpPost]
        public IActionResult CrearCuenta([FromBody] CuentaBancariaDTO dto)
        {
            if (dto == null)
                return BadRequest("Datos inválidos.");

            // Validar que el usuario exista
            var usuario = _context.Usuarios.Find(dto.UsuarioId);
            if (usuario == null)
                return BadRequest("El usuario especificado no existe.");

            // Validar número de cuenta único
            bool numeroExiste = _context.CuentasBancarias.Any(c => c.NumeroCuenta == dto.NumeroCuenta);
            if (numeroExiste)
                return BadRequest("El número de cuenta ya existe.");

            // Crear la cuenta
            var cuenta = new CuentaBancaria
            {
                NumeroCuenta = dto.NumeroCuenta,
                Saldo = dto.Saldo,
                UsuarioId = dto.UsuarioId
            };

            _context.CuentasBancarias.Add(cuenta);
            _context.SaveChanges();

            return Ok(new { message = "Cuenta creada correctamente." });
        }
    }
}