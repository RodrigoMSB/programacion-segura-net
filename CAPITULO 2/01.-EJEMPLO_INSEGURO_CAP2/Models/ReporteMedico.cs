// ================================================================
//   ReporteMedico.cs — Modelo de dominio (Versión INSEGURA)
// ================================================================
// Este archivo define la entidad ReporteMedico para el ejemplo
// inseguro del Capítulo 2. Representa información confidencial
// de pacientes sin aplicar validaciones ni restricciones de
// confidencialidad ni controles de acceso. 
// Su propósito es ilustrar los riesgos de no levantar ni
// documentar requisitos de seguridad.
// ================================================================

namespace EjemploInseguroCapitulo2.Models
{
    /// <summary>
    /// Entidad que modela un reporte médico de un paciente.
    /// Esta clase carece de validaciones y mecanismos de protección de datos sensibles.
    /// Se usa como ejemplo de malas prácticas cuando no se identifican activos críticos.
    /// </summary>
    public class ReporteMedico
    {
        /// <summary>
        /// Identificador único del reporte médico.
        /// Es generado automáticamente por la base de datos en memoria.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre completo del paciente asociado al reporte.
        /// No se valida ni se protege este dato sensible.
        /// </summary>
        public string Paciente { get; set; } = string.Empty;

        /// <summary>
        /// Diagnóstico médico del paciente.
        /// Se almacena tal cual se recibe, sin restricciones ni control de acceso.
        /// </summary>
        public string Diagnostico { get; set; } = string.Empty;

        /// <summary>
        /// Observaciones adicionales relacionadas con el estado del paciente.
        /// No se filtra ni se restringe su acceso.
        /// </summary>
        public string Observaciones { get; set; } = string.Empty;
    }
}