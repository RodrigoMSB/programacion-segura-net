
# 🛡️ SeguridadBancoFinal — Sistema Seguro de Transferencias Bancarias (.NET 8 + SQLite)

Este proyecto implementa un sistema seguro de gestión bancaria con autenticación JWT, encriptación de contraseñas y control de roles. Está diseñado con prácticas modernas de desarrollo seguro en .NET 8 y expone una API RESTful.

---

## 🚀 Pasos para levantar el sistema

### 1. Clonar el repositorio y entrar a la carpeta raíz del proyecto
```bash
git clone https://github.com/usuario/SeguridadBancoFinal.git
cd SeguridadBancoFinal
```

### 2. Confiar en el certificado de desarrollo HTTPS
```bash
dotnet dev-certs https --trust
```

### 3. Restaurar, limpiar y construir el proyecto
```bash
dotnet clean
dotnet build
```

### 4. Crear la migración inicial (si es primera vez)
```bash
dotnet ef migrations add InitialCreate
```

### 5. Aplicar la migración y crear la base de datos
```bash
dotnet ef database update
```

### 6. Ejecutar el sistema
```bash
dotnet run
```

---

## 🔄 Resetear la base de datos manualmente

Puedes resetear el sistema eliminando las tablas manualmente si es necesario:

```sql
DROP TABLE IF EXISTS AuditoriasTransferencias;
DROP TABLE IF EXISTS Movimientos;
DROP TABLE IF EXISTS CuentasBancarias;
DROP TABLE IF EXISTS Usuarios;
```

También puedes borrar el archivo físico `SeguridadBancoFinal.db` y volver a correr:

```bash
dotnet ef database update
```

---

## 🔐 Configuración de Seguridad

El archivo `appsettings.json` define las claves de configuración:

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

## 🧪 Pruebas con Postman

- **No se incluye Swagger.**
- Se utiliza **Postman** para probar los endpoints de la API.
- Las colecciones de Postman se adjuntan como parte del ejercicio práctico.
- Todos los endpoints requieren un token JWT en la mayoría de los casos.
- Asegúrate de autenticar primero (POST /auth/login) y luego usar el token en los headers.

---

## 📦 Tecnologías utilizadas

- ASP.NET Core 8
- SQLite + Entity Framework Core
- FluentValidation
- JWT Authentication
- HTTPS con certificados dev
- Middleware personalizado para manejo de excepciones

---

## 📁 Estructura del Proyecto (simplificada)

```
SeguridadBancoFinal/
├── Controllers/
├── DTOs/
├── Middleware/
├── Models/
├── Services/
├── Validation/
├── Data/
├── Program.cs
└── appsettings.json
```

---

## 🔐 Endpoints principales

| Tipo | Ruta | Descripción |
|------|------|-------------|
| POST | `/auth/login` | Login de usuario |
| POST | `/auth/register` | Registro (admin o cliente) |
| GET  | `/cuenta/saldo` | Ver saldos del usuario logueado |
| POST | `/transferencia/enviar` | Enviar transferencia |
| GET  | `/transferencia/mis` | Ver movimientos personales |
| GET  | `/admin/usuarios` | Ver usuarios (admin) |
| GET  | `/admin/auditorias` | Ver auditorías (admin) |

> Para ver todos los endpoints, abre Swagger en:  
> 👉 https://localhost:5001/swagger

---

## 🛠 Configuración por defecto (`appsettings.json`)

```json
{
  "JwtSettings": {
    "Secret": "TuClaveSecretaLarga123456789!",
    "LifespanMinutes": 60
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=banco_seguro.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

---

## 🧪 Tips para desarrollo

- El sistema usa **CORS** para permitir peticiones desde `http://localhost:5173`.
- Puedes consultar o editar la base de datos `.db` con [DB Browser for SQLite](https://sqlitebrowser.org/).
- Las contraseñas están hasheadas y salteadas con PBKDF2 y SHA-256.

## ✅ Autores y licencia

Este proyecto fue desarrollado para fines didácticos y puede ser adaptado y extendido para uso profesional.
