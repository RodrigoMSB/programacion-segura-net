# ✅ Ejemplo Inseguro — Capítulo 7

## 📌 Descripción

Este proyecto demuestra **cómo NO se debe manejar una sesión** en aplicaciones ASP.NET Core. Se incluyen configuraciones y prácticas intencionalmente inseguras para contrastarlas con una implementación segura.

### Problemas ilustrados:
- La sesión se configura sin expiración razonable (`IdleTimeout` extremadamente largo).
- Las cookies de sesión no usan banderas de seguridad (`HttpOnly`, `Secure`, `SameSite`).
- El endpoint de logout solo limpia el servidor, pero **no revoca ni borra la cookie en el cliente**.
- No se aplica HTTPS ni redirección forzada.

## ⚙️ Ejecución

1. Restaurar dependencias:
   ```bash
   dotnet restore
   ```

2. Compilar el proyecto:
   ```bash
   dotnet build
   ```

3. Ejecutar:
   ```bash
   dotnet run
   ```

4. Acceder vía navegador o Postman:
   ```
   http://localhost:5000
   ```

## 🔑 Endpoints inseguros

- **POST `/session/login?username=Rodrigo`**
  - Inicia una sesión insegura. No valida credenciales.
  - El Session ID no se regenera.

- **GET `/session/perfil`**
  - Muestra datos de sesión si existe.
  - Nunca expira automáticamente.

- **POST `/session/logout`**
  - Intenta cerrar la sesión limpiando datos del servidor.
  - No invalida ni elimina la cookie en cliente.

## 🚫 Importante

> ⚠️ **Este ejemplo es 100% didáctico.**
> Fue construido para mostrar malas prácticas de manejo de sesión.
> **NO se debe usar en producción ni como base para proyectos reales.**

## ✅ Siguiente paso

Contrasta este ejemplo con la **Versión Segura del Capítulo 7**, donde aplicarás:
- Cookies protegidas (`HttpOnly`, `Secure`, `SameSite`).
- Expiración controlada de sesión.
- Regeneración de Session ID.
- Logout seguro que invalida la cookie.
- Redirección y políticas HSTS.

**Autor: Equipo de Curso — Prácticas Seguras en .NET Core**