
# 🧠 Notas Técnicas — Backend SeguridadBancoFinal

Este archivo resume los conceptos clave aplicados en el backend del sistema SeguridadBancoFinal, explicando su propósito, tecnologías utilizadas y buenas prácticas integradas.

---

## 📦 Arquitectura del Proyecto

El backend está desarrollado en **.NET 8**, bajo un enfoque de **Clean Architecture ligera**, con las siguientes capas principales:

- `Models`: Define las entidades del dominio.
- `DTOs`: Define objetos de transferencia de datos (evita exponer entidades directamente).
- `Data`: Contiene el `ApplicationDbContext` para EF Core.
- `Services`: Implementa la lógica de negocio.
- `Controllers`: Define los endpoints de la API RESTful.
- `Validation`: Define reglas de validación desacopladas con FluentValidation.
- `Middleware`: Middleware personalizado para manejo centralizado de excepciones.

---

## 🔐 Seguridad

- **Autenticación JWT**:
  - Basado en tokens firmados con `HS256`.
  - Se configura en `Program.cs` y se genera en `AuthService`.
  - Configuración en `appsettings.json` bajo la clave `JwtSettings`.

- **Autorización por Roles**:
  - Uso de `[Authorize(Roles = "...")]` en endpoints que requieren privilegios específicos.
  - Roles definidos directamente en el campo `Rol` de la entidad `Usuario`.

- **Hash de Contraseñas**:
  - Uso de `PBKDF2` con SHA256 y 100.000 iteraciones.
  - Implementado en `CryptoService`.
  - Las contraseñas no se almacenan en texto plano.

- **Auditoría de Transferencias**:
  - Se guarda IP de origen, email del usuario y datos completos de la transacción.
  - Implementado en `TransferenciaService`.

---

## 🧪 Validación

- Se emplea **FluentValidation** para desacoplar las validaciones del modelo.
- Validators:
  - `LoginRequestValidator`
  - `RegisterRequestValidator`
  - `TransferenciaRequestValidator`

Esto permite una validación clara, reutilizable y mantenible.

---

## ⚙️ Entity Framework Core

- Motor de base de datos: **SQLite** (por simplicidad y portabilidad).
- Se configura en `Program.cs` y se inyecta `ApplicationDbContext`.
- Tablas:
  - `Usuarios`
  - `CuentasBancarias`
  - `Movimientos`
  - `AuditoriasTransferencias`

Relaciones:
- Un usuario tiene muchas cuentas.
- Un movimiento tiene cuenta origen y destino.
- Las claves foráneas usan `Restrict` para evitar borrado en cascada accidental.

---

## 💥 Middleware Personalizado

- Clase `ExceptionHandlingMiddleware`:
  - Intercepta errores no controlados.
  - Registra los errores en consola y responde con un JSON genérico seguro.
  - Mejora trazabilidad y evita filtrado de stack traces a usuarios.

---

## 🧪 Pruebas y API Usage

- Aunque no se usa Swagger, se trabaja con **Postman**.
- Las colecciones de Postman están adjuntas al ejercicio (ver carpeta `Postman/`).
- Se recomienda usar tokens JWT en la pestaña "Authorization" -> "Bearer Token".

---

## ♻️ Reset del Sistema

Para dejar el sistema desde cero:

```bash
dotnet dev-certs https --trust
dotnet clean
dotnet build
dotnet run
```

Para generar las tablas (primera vez):

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Para eliminar las tablas:

```sql
DROP TABLE IF EXISTS Movimientos;
DROP TABLE IF EXISTS CuentasBancarias;
DROP TABLE IF EXISTS Usuarios;
```

---

## 📁 appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=SeguridadBancoFinal.db"
  },
  "JwtSettings": {
    "Secret": "SuperClaveUltraMegaHiperSecreta123456789!",
    "LifespanMinutes": "60"
  }
}
```

---

## 🧠 Buenas prácticas destacadas

- Separación de responsabilidades por capas.
- Uso de DTOs en lugar de exponer entidades.
- Validación robusta con FluentValidation.
- Manejo explícito de errores y trazabilidad.
- Hash de contraseñas seguro con PBKDF2.
- Registro de auditorías para trazabilidad y cumplimiento.
