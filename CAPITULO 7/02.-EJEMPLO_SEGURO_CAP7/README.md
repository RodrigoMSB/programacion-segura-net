# ✅ Ejemplo SEGURO — Capítulo 7

## 📌 Descripción

Este proyecto muestra cómo aplicar un **manejo seguro de sesiones** en ASP.NET Core. Contrasta con la versión insegura para reforzar buenas prácticas:

- Cookies de sesión protegidas (`HttpOnly`, `SecurePolicy = Always`, `SameSite = Strict`).
- Expiración de sesión razonable (`IdleTimeout` = 20 minutos).
- Regeneración del Session ID simulada al iniciar sesión.
- Logout efectivo que invalida datos en servidor y cliente.
- HTTPS Redirection y HSTS habilitados.

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

4. Acceder vía navegador o Postman en:
   ```
   http://localhost:5000
   ```

## 🔑 Endpoints

### POST `/sessionseguro/login`

- **Qué hace:** Inicia sesión segura y regenera Session ID.
- **Parámetro:** `username` como query string.
- **Body:** **No necesita Body**.
- **Ejemplo URL:** `http://localhost:5000/sessionseguro/login?username=Rodrigo`.

### GET `/sessionseguro/perfil`

- **Qué hace:** Retorna el perfil si la sesión es válida.
- **Requiere cookie de sesión enviada automáticamente por Postman**.
- **Body:** Ninguno.

### POST `/sessionseguro/logout`

- **Qué hace:** Limpia la sesión en servidor y obliga a la cookie a expirar.
- **Body:** Ninguno.

## 🧩 Uso en Postman

- Importa la colección `.postman_collection.json` proporcionada.
- Ejecuta en orden:
  1. **Login:** envía el query `username`.
  2. **Perfil:** verifica que devuelve perfil si la cookie es válida.
  3. **Logout:** cierra la sesión.
  4. **Reintenta Perfil:** debe devolver 401.

> **Tip:** Verifica que Postman mantenga la cookie de sesión entre peticiones (Collection Settings → Cookies habilitadas).

## 🚫 Importante

Este ejemplo es didáctico. Para producción:
- Considera usar un cache distribuido (Redis).
- Implementa protección antiforgery en formularios POST reales.
- Agrega encabezados CSP y X-Frame-Options.

---

**Autor: Equipo de Curso — Prácticas Seguras en .NET Core**