# Ejemplo Inseguro — Capítulo 3

---

## Descripción General

Este proyecto forma parte del **Capítulo 3: Diseño y Arquitectura Segura de Aplicaciones**.  
Demuestra de forma intencional cómo luce una **arquitectura deficiente** en términos de seguridad:

- No aplica separación de responsabilidades: la lógica de negocio y el acceso a datos están mezclados en un mismo controlador.
- Expone datos sensibles y secretos directamente en el código fuente (claves y tokens hardcodeados).
- Permite acceso irrestricto a información confidencial, sin roles, claims ni autenticación básica.
- Viola principios de diseño seguro como defensa en profundidad y principio de menor privilegio.

Sirve como base de comparación directa con la versión segura desarrollada para este mismo capítulo.

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

## Endpoints inseguros disponibles

| Método | Ruta | Descripción |
| ------ | ---- | ------------ |
| POST | `/usuario/crear` | Crea un usuario sin validar campos ni roles. |
| GET | `/usuario/todos` | Lista todos los usuarios sin ninguna restricción de acceso. |
| GET | `/usuario/secreto` | Devuelve un secreto de API incrustado en el código fuente. |

---

## Cómo probarlo en Postman

### 1️⃣ Crear un Usuario (Inseguro)

- **Método:** POST  
- **URL:** `http://localhost:5000/usuario/crear`
- **Body (JSON):**
  ```json
  {
    "id": 1,
    "nombre": "Hacker",
    "clave": "abc123",
    "rol": "Admin"
  }
  ```
- **Headers:** `Content-Type: application/json`

Este request crea un usuario con cualquier rol y clave, sin validación.

### 2️⃣ Obtener Todos los Usuarios

- **Método:** GET  
- **URL:** `http://localhost:5000/usuario/todos`
- **Headers:** No requiere ninguno.

Este request devuelve toda la lista de usuarios sin necesidad de autenticación.

### 3️⃣ Obtener Secreto Hardcodeado

- **Método:** GET  
- **URL:** `http://localhost:5000/usuario/secreto`
- **Headers:** No requiere ninguno.

Devuelve un valor secreto codificado directamente en el controlador, lo que demuestra una mala práctica grave.

---

## Observaciones Importantes

- Este ejemplo se creó exclusivamente con fines **didácticos** para ilustrar **anti-patrones de seguridad**.
- Bajo ninguna circunstancia debe usarse como base para ambientes de producción.
- Analízalo en conjunto con el **NOTAS.md** para identificar claramente los riesgos y contrastarlos con las prácticas correctas del **Ejemplo Seguro — Capítulo 3**.