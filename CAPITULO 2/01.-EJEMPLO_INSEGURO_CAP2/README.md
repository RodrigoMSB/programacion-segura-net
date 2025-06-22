# Ejemplo Inseguro — Capítulo 2

---

## Descripción general

Este proyecto corresponde al **Capítulo 2** del curso y demuestra cómo se ve una API cuando **no se realiza un levantamiento de requisitos de seguridad adecuado**.

Simula un sistema de **gestión de reportes médicos confidenciales**, expuestos sin:

- Identificación ni clasificación de activos críticos.
- Definición de actores y permisos.
- Control de acceso y validación de datos.
- Documentación de amenazas ni políticas de cumplimiento.

Sirve como base para comparar con la versión segura del mismo capítulo.

---

## Instrucciones de ejecución

1. Abrir una terminal en la raíz del proyecto.
2. Restaurar dependencias:
   ```bash
   dotnet restore
   ```
3. Compilar:
   ```bash
   dotnet build
   ```
4. Ejecutar:
   ```bash
   dotnet run
   ```
5. Acceder a la API en:
   ```
   http://localhost:5000
   ```

---

## Endpoints disponibles

| Método | Ruta | Descripción |
| ------ | ---- | ------------ |
| POST | `/reportemedico/crear` | Crea un nuevo reporte médico sin validación ni restricciones de acceso. |
| GET | `/reportemedico/todos` | Devuelve todos los reportes médicos existentes, sin autenticación. |

---

## Cómo usar la colección Postman

Para comprobar la inseguridad de la API de forma práctica:

1. Abrir Postman.
2. Clic en **Importar**.
3. Seleccionar el archivo:
   `01.-EJEMPLO_INSEGURO_CAP2.json`.
4. Postman cargará las solicitudes agrupadas por nombre.

### Solicitudes incluidas:

- **Crear Reporte Médico**
  - Método: POST
  - URL: `http://localhost:5000/reportemedico/crear`
  - Body (JSON):
    ```json
    {
      "paciente": "Juan Pérez",
      "diagnostico": "Hipertensión",
      "observaciones": "Control mensual recomendado."
    }
    ```

- **Obtener Todos los Reportes**
  - Método: GET
  - URL: `http://localhost:5000/reportemedico/todos`

### Observación:

- No se requiere token ni autenticación.
- Se puede manipular cualquier campo sin restricción.
- Muestra claramente la ausencia de un levantamiento de requisitos de seguridad sólido.

---

## Importante

Este proyecto es **exclusivo para fines académicos** y NO debe utilizarse como base de desarrollo en producción.

Su propósito es servir de contraste con la versión segura del Capítulo 2.
