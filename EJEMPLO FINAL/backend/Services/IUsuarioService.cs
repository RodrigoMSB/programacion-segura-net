using SeguridadBancoFinal.Models;

namespace SeguridadBancoFinal.Services
{
    public interface IUsuarioService
    {
        Usuario RegistrarUsuario(string nombre, string email, string passwordPlano, string rol = "Cliente");
        Usuario? ValidarCredenciales(string email, string passwordPlano);
        Usuario CrearClienteSinPassword(string nombre, string email);
        Usuario? ObtenerPorId(int id);
        Usuario? ObtenerPorEmail(string email);
        IEnumerable<Usuario> ObtenerTodos();
        void CompletarRegistroCliente(string email, string password);
    }
}