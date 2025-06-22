# Ejemplo Inseguro — Capítulo 5

---

## Descripción

Este proyecto demuestra una **implementación intencionalmente deficiente de autenticación y autorización** para evidenciar las consecuencias de no aplicar controles básicos de seguridad en una API expuesta a internet.

### Problemas intencionales:
- No se valida ninguna credencial de usuario.
- Se permite al cliente declarar su propio rol (incluso 'Admin').
- No se usa JWT, cookies seguras ni ningún mecanismo de sesión.
- Endpoints críticos permanecen abiertos para cualquier solicitante.

Este escenario sirve como base de comparación directa con la versión segura, donde se aplican autenticación JWT, control de roles y políticas de autorización.

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

## Endpoints inseguros disponibles

| Método | Ruta | Descripción |
| ------ | ---- | ------------ |
| POST | `/auth/login` | Simula inicio de sesión sin validar credenciales. El cliente puede enviar su rol libremente. |
| GET | `/auth/recurso-admin` | Retorna recurso restringido sin verificar tokens ni roles reales. |

---

## Cómo probar en Postman

### 1️⃣ Probar login inseguro

- **Método:** POST  
- **URL:** `http://localhost:5000/auth/login`  
- **Header:** `Content-Type: application/json`  
- **Body (raw JSON):**
  ```json
  {
    "nombre": "UsuarioInseguro",
    "contrasena": "1234",
    "rol": "Admin"
  }
  ```

**Resultado esperado:**  
Retorna `200 OK` y confirma acceso con rol 'Admin' sin ningún control.

---

### 2️⃣ Probar acceso a recurso de administrador

- **Método:** GET  
- **URL:** `http://localhost:5000/auth/recurso-admin`

**Resultado esperado:**  
Cualquier cliente obtiene acceso, sin token ni sesión válida.

---

## Importante

Este proyecto existe exclusivamente con fines didácticos para analizar malas prácticas de autenticación y autorización.  
Sirve para contrastar con la versión segura del Capítulo 5, donde se aplican JWT firmados, validación de credenciales y control de acceso basado en roles de forma robusta.

---

**Uso exclusivo para formación en prácticas seguras de desarrollo de software.**