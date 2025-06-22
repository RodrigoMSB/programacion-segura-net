// ================================================================
//   ReporteMedicoController.cs — Controlador (Versión SEGURA)
// ================================================================
// Este archivo define el controlador para la API de reportes médicos,
// implementando validación de entrada y verificación de roles para
// proteger datos sensibles y aplicar principios de seguridad desde
// el levantamiento de requisitos.
// ================================================================

using EjemploSeguroCapitulo2.Models;
using EjemploSeguroCapitulo2.Services;
using Microsoft.AspNetCore.Mvc;

namespace EjemploSeguroCapitulo2.Controllers
{
    /// <summary>
    /// Controlador principal para la gestión de reportes médicos.
    /// Esta versión aplica validación de datos y control de acceso
    /// basado en roles, alineándose con el levantamiento de requisitos seguro.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReporteMedicoController : ControllerBase
    {
        /// <summary>
        /// Servicio encargado de manejar la lógica de negocio para reportes médicos.
        /// Implementa separación de responsabilidades.
        /// </summary>
        private readonly ReporteMedicoService _servicio;

        /// <summary>
        /// Constructor que inyecta el servicio de reportes médicos.
        /// </summary>
        /// <param name="servicio">Instancia de ReporteMedicoService.</param>
        public ReporteMedicoController(ReporteMedicoService servicio)
        {
            _servicio = servicio;
        }

        /// <summary>
        /// Endpoint para crear un nuevo reporte médico.
        /// Valida los datos de entrada usando las reglas definidas en el modelo.
        /// </summary>
        /// <param name="reporte">Datos del reporte médico enviados por el cliente.</param>
        /// <returns>Mensaje de confirmación o errores de validación.</returns>
        [HttpPost("crear")]
        public IActionResult Crear([FromBody] ReporteMedico reporte)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _servicio.Crear(reporte);
            return Ok("Reporte médico creado de forma segura.");
        }

        /// <summary>
        /// Endpoint para obtener todos los reportes médicos almacenados.
        /// Requiere rol 'Medico' enviado mediante header para autorizar la consulta.
        /// Si el rol es inválido o no se especifica, devuelve error de acceso.
        /// </summary>
        /// <param name="rol">Rol del solicitante (debe ser 'Medico').</param>
        /// <returns>Lista de reportes médicos o error de autorización.</returns>
        [HttpGet("todos")]
        public IActionResult ObtenerTodos([FromHeader] string rol)
        {
            try
            {
                var reportes = _servicio.ObtenerTodos(rol);
                return Ok(reportes);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}