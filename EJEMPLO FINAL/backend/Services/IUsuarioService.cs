// ===================================================================
// IUsuarioService.cs — Contrato de Servicio para Gestión de Usuarios
// ===================================================================
// Esta interfaz define el contrato de operaciones para la gestión de
// usuarios en el sistema, separando la lógica de implementación de
// su uso. Establece un conjunto de funcionalidades críticas:
// 
// - Registro de nuevos usuarios con o sin contraseña.
// - Validación de credenciales de login.
// - Consulta de usuarios por ID o email.
// - Listado general (usado por administradores).
// - Asignación de contraseña posterior al registro.
// - Gestión de cuentas bancarias asociadas.
//
// El uso de esta interfaz permite:
// - Inversión de dependencias (principio DIP).
// - Facilitar testeo unitario por mocking.
// - Mantener contratos estables ante refactors.
//
// ===================================================================

using SeguridadBancoFinal.Models;

namespace SeguridadBancoFinal.Services
{
    /// <summary>
    /// Define el conjunto de operaciones que deben ser implementadas
    /// para la gestión segura de usuarios dentro del sistema bancario.
    /// </summary>
    public interface IUsuarioService
    {
        /// <summary>
        /// Registra un nuevo usuario completo con contraseña segura.
        /// </summary>
        /// <param name="nombre">Nombre del usuario.</param>
        /// <param name="email">Email único del usuario.</param>
        /// <param name="passwordPlano">Contraseña en texto claro (será hasheada).</param>
        /// <param name="rol">Rol asignado (por defecto: "Cliente").</param>
        /// <returns>Entidad Usuario recién registrada.</returns>
        Usuario RegistrarUsuario(string nombre, string email, string passwordPlano, string rol = "Cliente");

        /// <summary>
        /// Valida las credenciales de un usuario contra la base de datos.
        /// </summary>
        /// <param name="email">Email ingresado en login.</param>
        /// <param name="passwordPlano">Contraseña ingresada (en claro).</param>
        /// <returns>Usuario si las credenciales son correctas; null si no.</returns>
        Usuario? ValidarCredenciales(string email, string passwordPlano);

        /// <summary>
        /// Crea un usuario tipo cliente sin contraseña asignada.
        /// Utilizado por un administrador para completar luego.
        /// </summary>
        /// <param name="nombre">Nombre del cliente.</param>
        /// <param name="email">Email único del cliente.</param>
        /// <returns>Entidad Usuario creada.</returns>
        Usuario CrearClienteSinPassword(string nombre, string email);

        /// <summary>
        /// Busca y retorna un usuario por su identificador numérico.
        /// </summary>
        /// <param name="id">ID único del usuario.</param>
        /// <returns>Usuario si existe; null si no.</returns>
        Usuario? ObtenerPorId(int id);

        /// <summary>
        /// Busca un usuario por su email registrado.
        /// </summary>
        /// <param name="email">Email del usuario.</param>
        /// <returns>Usuario correspondiente o null.</returns>
        Usuario? ObtenerPorEmail(string email);

        /// <summary>
        /// Devuelve todos los usuarios del sistema. Solo uso interno o para administradores.
        /// </summary>
        /// <returns>Enumeración de usuarios.</returns>
        IEnumerable<Usuario> ObtenerTodos();

        /// <summary>
        /// Completa el registro de un cliente al asignarle contraseña segura.
        /// </summary>
        /// <param name="email">Email del cliente pendiente.</param>
        /// <param name="password">Contraseña en claro (será hasheada).</param>
        void CompletarRegistroCliente(string email, string password);

        /// <summary>
        /// Retorna una cuenta bancaria a partir de su número único.
        /// </summary>
        /// <param name="numeroCuenta">Número de cuenta bancaria.</param>
        /// <returns>CuentaBancaria si existe; null si no.</returns>
        CuentaBancaria? ObtenerCuentaPorNumero(string numeroCuenta);
    }
}