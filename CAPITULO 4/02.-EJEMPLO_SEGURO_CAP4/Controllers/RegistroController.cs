// ================================================================
//   RegistroController.cs — Controlador (Versión SEGURA)
// ================================================================
// Este archivo define el controlador para procesar el formulario
// de registro en el Ejemplo SEGURO — Capítulo 4.
//
// Implementa prácticas correctas:
// - Valida automáticamente el modelo usando ModelState.
// - Retorna errores estructurados cuando la entrada no cumple.
// - Aplica reglas de Data Annotations y FluentValidation de forma combinada.
// - Mantiene la lógica de negocio separada y clara.
//
// Este enfoque reduce el riesgo de procesar entradas maliciosas
// y refuerza la integridad de la API.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres utilizados
// ---------------------------------------------------------------

// Importa el modelo de dominio que define los campos y reglas
// de validación básica mediante Data Annotations.
using EjemploSeguroCapitulo4.Models;

// Importa el espacio de nombres que contiene la infraestructura
// para construir controladores API REST en ASP.NET Core.
using Microsoft.AspNetCore.Mvc;

namespace EjemploSeguroCapitulo4.Controllers
{
    /// <summary>
    /// Controlador responsable de exponer el endpoint para
    /// registrar un nuevo usuario. Aplica validación robusta
    /// combinando Data Annotations y FluentValidation.
    /// </summary>
    [ApiController] // Activa características como validación automática de ModelState.
    [Route("[controller]")] // Define la ruta base del controlador: /registro
    public class RegistroController : ControllerBase
    {
        /// <summary>
        /// Endpoint HTTP POST para crear un nuevo registro de usuario.
        /// 
        /// Proceso:
        /// - ASP.NET Core valida automáticamente el modelo según
        ///   los atributos de Data Annotations.
        /// - FluentValidation amplía esa validación con reglas
        ///   más avanzadas (longitud de contraseña, complejidad).
        /// - Si hay errores, devuelve un BadRequest con los detalles.
        /// - Si todo es válido, devuelve una respuesta de éxito.
        /// </summary>
        /// <param name="usuario">
        /// Objeto UsuarioRegistro que representa los datos enviados
        /// por el cliente en el cuerpo de la solicitud.
        /// </param>
        /// <returns>
        /// HTTP 200 OK si la validación es correcta; de lo contrario,
        /// HTTP 400 Bad Request con los detalles de validación.
        /// </returns>
        [HttpPost("crear")]
        public IActionResult Crear([FromBody] UsuarioRegistro usuario)
        {
            // -------------------------------------------------------
            // VALIDACIÓN DEL MODELO
            // -------------------------------------------------------
            // Verifica si el modelo cumple todas las reglas de Data Annotations
            // y FluentValidation. Si no es válido, retorna inmediatamente.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // -------------------------------------------------------
            // LÓGICA DE NEGOCIO SEGURA (SIMULADA)
            // -------------------------------------------------------
            // Si pasa la validación, se ejecuta la lógica de negocio.
            // Aquí solo se simula una respuesta de éxito.
            return Ok($"Registro exitoso para {usuario.Nombre} con correo {usuario.Email}.");
        }
    }
}