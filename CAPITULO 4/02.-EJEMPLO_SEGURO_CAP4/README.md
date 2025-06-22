# Ejemplo Seguro — Capítulo 4

---

## Descripción General

Este proyecto demuestra cómo construir un formulario de registro **seguro y robusto** usando herramientas y prácticas recomendadas en **.NET Core**.  
Aplica una combinación de validación declarativa y validación avanzada para garantizar que solo datos correctos pasen a la lógica de negocio.

### Incluye:
- ✅ **Data Annotations**: Reglas básicas aplicadas directamente en el modelo (`[Required]`, `[StringLength]`, `[EmailAddress]`).
- ✅ **FluentValidation**: Reglas más complejas para la contraseña (mínima longitud, combinación de caracteres).
- ✅ **Validación con `ModelState`**: Bloquea solicitudes inválidas antes de procesarlas.
- ✅ **Manejo de errores estructurado**: Respuestas claras indicando qué falló y por qué.

---

## Instrucciones de Ejecución

1. Abrir una terminal en la raíz del proyecto.
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
5. Acceder a la API en:
   ```
   http://localhost:5000
   ```

---

## Endpoint Seguro

| Método | Ruta | Descripción |
| ------ | ---- | ------------ |
| POST | `/registro/crear` | Registra un usuario solo si todos los campos cumplen con las reglas de validación. |

---

## Cómo probar en Postman

### 1️⃣ Solicitud válida

- **Método:** POST
- **URL:** `http://localhost:5000/registro/crear`
- **Header:**  
  `Content-Type: application/json`
- **Body (raw JSON):**
  ```json
  {
    "nombre": "Rodrigo Seguro",
    "email": "rodrigo@seguro.com",
    "contrasena": "Password123"
  }
  ```
**Resultado esperado:**  
HTTP 200 OK → `"Registro exitoso para Rodrigo Seguro con correo rodrigo@seguro.com."`

---

### 2️⃣ Solicitud inválida (campos con errores)

- **Método:** POST
- **URL:** `http://localhost:5000/registro/crear`
- **Header:**  
  `Content-Type: application/json`
- **Body (raw JSON):**
  ```json
  {
    "nombre": "Ro",
    "email": "correo_invalido",
    "contrasena": "abc"
  }
  ```
**Resultado esperado:**  
HTTP 400 Bad Request → JSON con lista de errores indicando que:
- El nombre es demasiado corto.
- El correo no es válido.
- La contraseña es demasiado débil.

---

## Importante

- Este ejemplo muestra **validación múltiple** en acción, asegurando que la aplicación filtre entradas maliciosas o mal formadas **antes de llegar a la lógica de negocio**.
- Comparar este proyecto con el **Ejemplo Inseguro — Capítulo 4** para comprender la diferencia entre una validación inexistente y una validación bien implementada.

---

**Uso exclusivo para prácticas educativas de seguridad en desarrollo de software.**