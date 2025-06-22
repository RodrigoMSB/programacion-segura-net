// ================================================================
//   ReporteMedico.cs — Modelo de dominio (Versión SEGURA)
// ================================================================
// Este archivo define la entidad ReporteMedico para el ejemplo
// SEGURO del Capítulo 2. Implementa validación de campos mediante
// anotaciones de datos para garantizar la calidad y protección
// de información sensible, aplicando los principios de seguridad
// desde el levantamiento de requisitos.
// ================================================================

using System.ComponentModel.DataAnnotations;

namespace EjemploSeguroCapitulo2.Models
{
    /// <summary>
    /// Entidad que representa un reporte médico confidencial.
    /// En esta versión se aplican validaciones de campos para
    /// evitar entradas inconsistentes y proteger datos críticos.
    /// </summary>
    public class ReporteMedico
    {
        /// <summary>
        /// Identificador único del reporte médico.
        /// Generado automáticamente por Entity Framework Core.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del paciente asociado al reporte.
        /// Validación: campo requerido, máximo 100 caracteres.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Paciente { get; set; } = string.Empty;

        /// <summary>
        /// Diagnóstico médico detallado del paciente.
        /// Validación: campo requerido, máximo 200 caracteres.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Diagnostico { get; set; } = string.Empty;

        /// <summary>
        /// Observaciones adicionales relacionadas con el reporte.
        /// Validación: máximo 500 caracteres.
        /// </summary>
        [StringLength(500)]
        public string Observaciones { get; set; } = string.Empty;
    }
}