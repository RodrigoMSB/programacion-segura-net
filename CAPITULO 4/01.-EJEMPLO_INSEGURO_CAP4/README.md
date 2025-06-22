# Ejemplo Inseguro — Capítulo 4

---

## Descripción General

Este proyecto forma parte del **Capítulo 4: Prácticas Seguras de Programación para Validación de Entradas**.  
Muestra de forma intencional cómo un formulario de registro mal implementado deja expuesta la aplicación a entradas no controladas y vulnerabilidades como XSS o inyección de comandos.

### Problemas demostrados:
- No se valida ningún campo del modelo.
- No existe restricción de formato, longitud ni coherencia.
- No hay sanitización de datos ni verificación en servidor.
- Respuestas generadas con los mismos datos enviados.
- Manejo de errores inexistente.

Sirve para contrastar con la versión segura donde se usan Data Annotations y FluentValidation.

---

## Instrucciones de Ejecución

1. Abrir una terminal en la carpeta raíz del proyecto.
2. Restaurar dependencias:
   ```bash
   dotnet restore
   ```
3. Compilar el proyecto:
   ```bash
   dotnet build
   ```
4. Ejecutar la aplicación:
   ```bash
   dotnet run
   ```
5. La API quedará disponible en:
   ```
   http://localhost:5000
   ```

---

## Endpoint Inseguro

| Método | Ruta | Descripción |
| ------ | ---- | ------------ |
| POST | `/registro/crear` | Acepta y procesa cualquier entrada sin validación ni sanitización. |

---

## Cómo probar en Postman

### 1️ Probar registro con datos válidos o maliciosos

- **Método:** POST
- **URL:** `http://localhost:5000/registro/crear`
- **Header:** `Content-Type: application/json`
- **Body (raw JSON):**
  ```json
  {
    "nombre": "Rodrigo <script>alert('xss')</script>",
    "email": "correo_invalido",
    "contrasena": "abc"
  }
  ```

**Resultado esperado:**  
- El servidor aceptará todos los campos tal como se enviaron, sin validar ni filtrar.
- La respuesta incluirá el script o entrada malformada, demostrando exposición a XSS.

### 2️ Probar campos excesivos

- Enviar cadenas muy largas para `nombre`, `email` o `contrasena` para verificar que no hay restricción de longitud ni manejo de errores por entradas voluminosas.

---

## Observaciones Importantes

- **No usar este proyecto como base para producción.**
- Es material de apoyo para evidenciar cómo la falta de validación permite ataques comunes.
- Contrastar con la versión segura permite reforzar buenas prácticas de validación y sanitización en .NET Core.

---

**Uso exclusivo para fines educativos y práctica de seguridad de software.**
