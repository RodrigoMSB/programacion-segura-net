// ================================================================
//   ReporteMedicoController.cs — Controlador (Versión INSEGURA)
// ================================================================
// Este archivo define el controlador de la API que permite crear y
// consultar reportes médicos confidenciales sin aplicar controles
// de seguridad, validación de datos ni autenticación.
// Su objetivo es demostrar los riesgos de no integrar la seguridad
// desde el levantamiento de requisitos.
// ================================================================

using EjemploInseguroCapitulo2.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjemploInseguroCapitulo2.Controllers
{
    /// <summary>
    /// Controlador principal para gestionar reportes médicos.
    /// Esta versión no implementa validaciones ni restricciones de acceso.
    /// Se expone públicamente para ilustrar malas prácticas.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReporteMedicoController : ControllerBase
    {
        /// <summary>
        /// Contexto de base de datos en memoria.
        /// Se usa directamente sin capa de servicio ni repositorio.
        /// </summary>
        private readonly ContextoBaseDatos _contexto;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos.
        /// </summary>
        public ReporteMedicoController(ContextoBaseDatos contexto)
        {
            _contexto = contexto;
        }

        /// <summary>
        /// Endpoint para crear un nuevo reporte médico.
        /// No realiza validación de campos ni verifica credenciales.
        /// </summary>
        /// <param name="reporte">Objeto ReporteMedico enviado por el cliente.</param>
        /// <returns>Mensaje de confirmación de creación.</returns>
        [HttpPost("crear")]
        public IActionResult CrearReporte(ReporteMedico reporte)
        {
            _contexto.ReportesMedicos.Add(reporte);
            _contexto.SaveChanges();
            return Ok("Reporte médico creado (INSEGURO)");
        }

        /// <summary>
        /// Endpoint para obtener la lista de todos los reportes médicos almacenados.
        /// No exige autenticación ni controla el acceso a datos sensibles.
        /// </summary>
        /// <returns>Lista de ReporteMedico existente en la base de datos en memoria.</returns>
        [HttpGet("todos")]
        public IActionResult ObtenerTodos()
        {
            return Ok(_contexto.ReportesMedicos.ToList());
        }
    }
}