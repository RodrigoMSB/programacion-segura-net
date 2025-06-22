namespace EjemploSeguroCapitulo6.Models
{
    /// <summary>
    /// DTO para recibir texto cifrado en Base64 dentro de un objeto JSON.
    /// </summary>
    public class DatoCifradoDTO
    {
        /// <summary>
        /// Propiedad que contiene el texto cifrado en Base64.
        /// </summary>
        public string Base64 { get; set; } = string.Empty;
    }
}