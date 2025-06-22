# Ejemplo Seguro — Capítulo 5

---

## Descripción

Este proyecto demuestra una **implementación robusta de autenticación y autorización** en una API REST utilizando **JSON Web Tokens (JWT)** y control de acceso basado en roles.

### Principales características:
- Valida credenciales de usuario durante el inicio de sesión.
- Emite un JWT firmado, que incluye claims de identidad y rol.
- Usa `[Authorize(Roles = "Admin")]` para restringir el acceso a rutas críticas.
- Requiere que el cliente envíe el token en el header `Authorization` para cada solicitud a rutas protegidas.

Este enfoque elimina la posibilidad de que el cliente defina su propio rol, en contraste con la versión insegura.

---

## Ejecución

1. Restaurar dependencias:
   ```bash
   dotnet restore
   ```

2. Compilar el proyecto:
   ```bash
   dotnet build
   ```

3. Ejecutar la aplicación:
   ```bash
   dotnet run
   ```

4. Acceder a la API en:
   ```
   http://localhost:5000
   ```

---

## Endpoints Seguros

| Método | Ruta | Descripción |
| ------ | ---- | ------------ |
| POST | `/auth/login` | Valida credenciales y devuelve un JWT firmado si son correctas. |
| GET | `/admin/recurso` | Endpoint protegido que solo permite acceso a usuarios con un JWT válido y rol `Admin`. |

---

## Cómo probar en Postman

### 1️⃣ Obtener un JWT válido

- **Método:** POST  
- **URL:** `http://localhost:5000/auth/login`  
- **Header:**  
  `Content-Type: application/json`
- **Body (raw JSON):**
  ```json
  {
    "nombre": "admin",
    "contrasena": "password123"
  }
  ```

**Resultado esperado:**  
Retorna `200 OK` con un objeto:
```json
{
  "token": "<JWT_AQUI>"
}
```

---

### 2️⃣ Acceder al recurso protegido

- **Método:** GET  
- **URL:** `http://localhost:5000/admin/recurso`  
- **Header:**  
  `Authorization: Bearer <Pega_Aqui_Tu_Token>`

**Resultado esperado:**  
Si el token es válido y el rol es correcto, devuelve `200 OK` con el mensaje:
"Acceso concedido al recurso de administrador con JWT válido y rol Admin."

Si el token es inválido, expirado o falta: devuelve `401 Unauthorized` o `403 Forbidden`.

---

## Importante

- Este ejemplo muestra la diferencia crítica con la versión insegura del Capítulo 5: aquí el servidor **controla el rol** y firma el token.
- **Nunca debes almacenar claves secretas directamente en el código fuente en un entorno real**. Usa variables de entorno o servicios como Azure Key Vault.
- Define tiempos de expiración razonables para minimizar riesgos si un token se ve comprometido.

---

**Uso exclusivo para prácticas de seguridad en desarrollo de software.**
