# Ejemplo Seguro — Capítulo 3

---

## Descripción General

Este proyecto ilustra la aplicación práctica de los **principios de diseño y arquitectura segura** estudiados en el Capítulo 3 del curso.  
A diferencia de la versión insegura, aquí se muestra cómo implementar:

- **Separación de responsabilidades:** La lógica de negocio está encapsulada en `UsuarioService`, manteniendo el controlador limpio y fácil de probar.
- **Validación robusta de entrada:** Uso de atributos de datos (`[Required]`, `[StringLength]`, `[RegularExpression]`) y verificación de `ModelState` antes de ejecutar cualquier operación.
- **Control de acceso y roles:** Simulación de roles (`Admin` y `User`) enviados como headers HTTP para controlar el acceso a recursos sensibles.
- **Gestión de secretos:** Uso de variables de entorno en lugar de claves hardcodeadas, siguiendo buenas prácticas de protección de configuración.

Este ejemplo contrasta directamente con la versión insegura y sirve como plantilla para construir APIs seguras en **.NET Core**.

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
5. Acceder a la API en:
   ```
   http://localhost:5000
   ```

---

## Endpoints Disponibles y Uso Correcto (Postman)

A continuación se detalla cómo probar cada endpoint usando **Postman** o cualquier cliente REST.  
Los headers y bodies deben respetarse para que la lógica de seguridad funcione correctamente.

---

### ✅ **1️⃣ Crear un Usuario**

- **Método:** `POST`
- **URL:** `http://localhost:5000/usuario/crear`
- **Headers:**  
  - `Content-Type: application/json`
- **Body (JSON):**
  ```json
  {
    "id": 1,
    "nombre": "UsuarioSeguro",
    "clave": "password123",
    "rol": "User"
  }
  ```

**Qué hace:**  
Registra un nuevo usuario. Si dejas campos vacíos, la API responderá con `400 Bad Request` y detalles de validación.

---

### ✅ **2️⃣ Obtener Todos los Usuarios**

- **Método:** `GET`
- **URL:** `http://localhost:5000/usuario/todos`
- **Headers:**  
  - `rol: Admin`  
    *o*  
  - `rol: User`

**Qué hace:**  
Devuelve la lista de todos los usuarios creados.  
Si el header `rol` está ausente o tiene un valor inválido, la API responderá con `401 Unauthorized`.

---

### ✅ **3️⃣ Obtener Secreto Protegido**

- **Método:** `GET`
- **URL:** `http://localhost:5000/usuario/secreto`
- **Headers:**  
  - `rol: Admin`

**Qué hace:**  
Devuelve el secreto simulado gestionado vía variable de entorno.  
**Importante:** Solo accesible para usuarios con rol `Admin`.  
Si usas `User` u omites el rol, obtendrás `401 Unauthorized`.

---

## Configuración de la Variable de Entorno (opcional)

Este ejemplo obtiene la clave API de la variable de entorno `API_KEY`.  
Si no la defines, usará `SECRET_SIMULADO` por defecto.  
Para probarlo con un valor real, puedes definirlo en tu terminal:

```bash
export API_KEY="MI_SUPER_API_KEY_SEGURA"
dotnet run
```

---

## Observaciones Finales

- Esta versión muestra cómo aplicar **mínimo privilegio**, **separación de responsabilidades** y **protección de configuración** en una API .NET Core.
- El control de roles se hace de forma didáctica usando headers, pero en escenarios reales se implementaría con JWT, Identity o un Identity Provider externo.
- Complementa este proyecto revisando el archivo `NOTAS.md` para entender cómo cada componente refleja los principios del **Capítulo 3**.

---

**Uso exclusivo para fines académicos y como ejemplo de buenas prácticas de arquitectura segura.**