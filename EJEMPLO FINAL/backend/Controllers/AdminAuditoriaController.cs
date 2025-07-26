// ===================================================
// IMPORTACIONES NECESARIAS PARA EL CONTROLADOR
// ===================================================

using Microsoft.AspNetCore.Authorization;          // Permite proteger el controlador mediante roles y políticas de seguridad
using Microsoft.AspNetCore.Mvc;                    // Proporciona atributos y clases para construir controladores API REST
using Microsoft.EntityFrameworkCore;               // Permite consultas asincrónicas y seguimiento de cambios en EF Core
using SeguridadBancoFinal.Data;                    // Acceso al DbContext definido en la capa de persistencia

// ===================================================
// CONTROLADOR DE AUDITORÍA PARA ADMINISTRADORES
// ===================================================

namespace SeguridadBancoFinal.Controllers
{
    /// <summary>
    /// Controlador para acceder a los registros de auditoría de transferencias.
    /// Restringido solo a usuarios con rol "Admin".
    /// </summary>
    [ApiController]
    [Route("admin/auditorias")]
    [Authorize(Roles = "Admin")]  // Acceso exclusivo a administradores
    public class AdminAuditoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de base de datos.
        /// </summary>
        /// <param name="context">Instancia del ApplicationDbContext para acceder a datos persistidos</param>
        public AdminAuditoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===================================================
        // GET: /admin/auditorias
        // ===================================================

        /// <summary>
        /// Devuelve la lista completa de registros de auditoría de transferencias.
        /// Ordenados desde el más reciente al más antiguo.
        /// </summary>
        /// <returns>Lista de auditorías en formato JSON</returns>
        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var auditorias = await _context.AuditoriasTransferencias
                .OrderByDescending(a => a.Fecha)
                .ToListAsync();

            return Ok(auditorias);
        }
    }
}