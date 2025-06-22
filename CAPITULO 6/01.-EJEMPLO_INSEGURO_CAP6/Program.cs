// ================================================================
//   Program.cs — Configuración principal (Versión INSEGURA)
// ================================================================
// Configura la aplicación web sin aplicar ninguna política de
// seguridad criptográfica ni gestión de secretos.
// No implementa almacenes de claves, ni configuración protegida.
// Sirve únicamente como base didáctica para evidenciar malas prácticas.
// ================================================================

// ---------------------------------------------------------------
// Crear un builder de aplicación web usando configuración por defecto.
// ---------------------------------------------------------------
var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------
// Registrar los controladores del proyecto.
// En este ejemplo, no se aplican filtros de seguridad, ni
// validaciones de entrada o políticas de autorización.
// ---------------------------------------------------------------
builder.Services.AddControllers();

// ---------------------------------------------------------------
// Construir la aplicación web a partir del builder.
// ---------------------------------------------------------------
var app = builder.Build();

// ---------------------------------------------------------------
// Mapear las rutas de los controladores para exponer los endpoints.
// No se usa autenticación ni middleware de seguridad adicional.
// ---------------------------------------------------------------
app.MapControllers();

// ---------------------------------------------------------------
// Ejecutar la aplicación en el servidor web Kestrel embebido.
// ---------------------------------------------------------------
app.Run();