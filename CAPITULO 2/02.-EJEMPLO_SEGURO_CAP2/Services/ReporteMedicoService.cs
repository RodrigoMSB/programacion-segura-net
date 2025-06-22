// ================================================================
//   ReporteMedicoService.cs — Lógica de negocio (Versión SEGURA)
// ================================================================
// Este archivo define la capa de servicio para la gestión de
// reportes médicos, aplicando separación de lógica de negocio,
// validación de roles y controles básicos de acceso.
// Representa un ejemplo de levantamiento de requisitos de seguridad
// bien estructurado.
// ================================================================

using EjemploSeguroCapitulo2.Models;

namespace EjemploSeguroCapitulo2.Services
{
    /// <summary>
    /// Servicio que encapsula la lógica de negocio relacionada con
    /// la creación y consulta de reportes médicos.
    /// Implementa controles de acceso básicos basados en rol.
    /// </summary>
    public class ReporteMedicoService
    {
        /// <summary>
        /// Contexto de base de datos en memoria.
        /// Se usa para persistir y consultar reportes.
        /// </summary>
        private readonly ContextoBaseDatos _contexto;

        /// <summary>
        /// Constructor que recibe el contexto de base de datos.
        /// </summary>
        /// <param name="contexto">Instancia de ContextoBaseDatos.</param>
        public ReporteMedicoService(ContextoBaseDatos contexto)
        {
            _contexto = contexto;
        }

        /// <summary>
        /// Método para registrar un nuevo reporte médico en la base de datos.
        /// Se asume que la validación de datos se realiza en el controlador.
        /// </summary>
        /// <param name="reporte">Objeto ReporteMedico validado.</param>
        public void Crear(ReporteMedico reporte)
        {
            _contexto.ReportesMedicos.Add(reporte);
            _contexto.SaveChanges();
        }

        /// <summary>
        /// Método para obtener todos los reportes médicos almacenados.
        /// Aplica verificación de rol: solo usuarios con rol 'Medico'
        /// pueden acceder a esta información sensible.
        /// </summary>
        /// <param name="rol">Rol del solicitante (debe ser 'Medico').</param>
        /// <returns>Lista de reportes médicos.</returns>
        /// <exception cref="UnauthorizedAccessException">
        /// Lanzada si el rol es inválido o insuficiente.
        /// </exception>
        public List<ReporteMedico> ObtenerTodos(string rol)
        {
            // Control de acceso básico: solo médicos autorizados
            if (rol != "Medico")
                throw new UnauthorizedAccessException("Acceso restringido: se requiere rol Médico.");
            
            return _contexto.ReportesMedicos.ToList();
        }
    }
}