// ================================================================
//   UsuarioRegistroValidator.cs — Validador FluentValidation
// ================================================================
// Este archivo define reglas de validación avanzada para el
// formulario de registro del Ejemplo SEGURO — Capítulo 4.
//
// Complementa las reglas básicas de Data Annotations aplicando
// lógica más compleja como validación de longitud, formato y
// políticas de complejidad de contraseña.
//
// Implementa el patrón de validación desacoplada, alineado con
// principios de Arquitectura Limpia.
// ================================================================

// ---------------------------------------------------------------
// IMPORTS — Espacios de nombres utilizados
// ---------------------------------------------------------------

// Importa el modelo de dominio que este validador evaluará.
using EjemploSeguroCapitulo4.Models;

// Importa la librería FluentValidation que provee una API fluida
// para definir reglas de validación condicionales y extensibles.
using FluentValidation;

namespace EjemploSeguroCapitulo4.Validators
{
    /// <summary>
    /// Clase que define un validador específico para el modelo
    /// UsuarioRegistro usando FluentValidation.
    /// Hereda de AbstractValidator<T> donde T es el tipo de modelo.
    /// </summary>
    public class UsuarioRegistroValidator : AbstractValidator<UsuarioRegistro>
    {
        /// <summary>
        /// Constructor del validador.
        /// Define todas las reglas de validación que complementan
        /// las Data Annotations del modelo de dominio.
        /// 
        /// Reglas aplicadas:
        /// - Nombre: no vacío, longitud entre 3 y 50 caracteres.
        /// - Email: no vacío, debe tener formato de correo válido.
        /// - Contraseña: no vacía, al menos 8 caracteres, incluye
        ///   una mayúscula, una minúscula y un número.
        /// </summary>
        public UsuarioRegistroValidator()
        {
            // -------------------------------------------------------
            // REGLA PARA EL CAMPO Nombre
            // -------------------------------------------------------
            RuleFor(x => x.Nombre)
                .NotEmpty() // No debe estar vacío
                .Length(3, 50); // Debe tener entre 3 y 50 caracteres

            // -------------------------------------------------------
            // REGLA PARA EL CAMPO Email
            // -------------------------------------------------------
            RuleFor(x => x.Email)
                .NotEmpty() // Obligatorio
                .EmailAddress(); // Formato válido de correo

            // -------------------------------------------------------
            // REGLA PARA EL CAMPO Contraseña
            // -------------------------------------------------------
            RuleFor(x => x.Contrasena)
                .NotEmpty() // Obligatorio
                .MinimumLength(8) // Al menos 8 caracteres
                .Matches("[A-Z]").WithMessage("Debe contener al menos una letra mayúscula.")
                .Matches("[a-z]").WithMessage("Debe contener al menos una letra minúscula.")
                .Matches("[0-9]").WithMessage("Debe contener al menos un número.");
        }
    }
}