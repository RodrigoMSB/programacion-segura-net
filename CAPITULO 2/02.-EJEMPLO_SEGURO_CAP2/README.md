# Ejemplo Seguro — Capítulo 2

---

## Descripción general

Este proyecto corresponde al **Capítulo 2** del curso y muestra cómo implementar de forma práctica un **levantamiento de requisitos de seguridad sólido**.

Simula una **API para gestionar reportes médicos confidenciales**, aplicando principios de:

- **Identificación de activos críticos:** Los campos del reporte se validan para evitar entradas maliciosas y proteger información sensible.
- **Definición de actores y roles:** Se controla el acceso a los endpoints mediante un rol simulado (`Medico`), restringiendo operaciones sensibles.
- **Validación de datos de entrada:** Uso de anotaciones de datos (`[Required]`, `[StringLength]`) y validación del `ModelState` en el controlador.
- **Separación de responsabilidades:** Lógica de negocio centralizada en una capa de servicio, siguiendo buenas prácticas de arquitectura.

Este ejemplo contrasta directamente con la versión insegura del mismo capítulo, para evidenciar la diferencia que genera un levantamiento de requisitos de seguridad bien ejecutado.

---

## Instrucciones de ejecución

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

## Endpoints disponibles

| Método | Ruta | Descripción |
| ------ | ---- | ------------ |
| POST | `/reportemedico/crear` | Registra un nuevo reporte médico. Valida campos obligatorios y longitud máxima. |
| GET | `/reportemedico/todos` | Devuelve todos los reportes médicos **solo si** el solicitante envía un rol válido (`Medico`). Accesos no autorizados son rechazados. |

---

## Cómo usar la colección Postman

Para probar de forma práctica el control de acceso y la validación:

1. Abrir Postman.
2. Importar el archivo:  
   `02.-EJEMPLO_SEGURO_CAP2_ES.postman_collection.json`
3. Ejecutar las solicitudes preconfiguradas:
   - **Crear Reporte Médico (Seguro):**
     - Método: POST
     - URL: `http://localhost:5000/reportemedico/crear`
     - Body (JSON):
       ```json
       {
         "paciente": "María González",
         "diagnostico": "Diabetes Tipo II",
         "observaciones": "Requiere control trimestral."
       }
       ```
   - **Obtener Todos los Reportes (Rol Médico):**
     - Método: GET
     - URL: `http://localhost:5000/reportemedico/todos`
     - Header: `rol` con valor `Medico`
   - **Obtener Todos los Reportes (Sin Rol o Rol Incorrecto):**
     - Método: GET
     - URL: `http://localhost:5000/reportemedico/todos`
     - Sin header o con rol diferente → Respuesta: error de acceso.

4. Observa cómo se valida la entrada y se restringe el acceso según el rol enviado.

---

## Aspectos importantes

- El rol se simula mediante un header HTTP para fines didácticos.
- Este diseño refleja cómo la identificación de activos, actores y superficies de ataque se traduce en decisiones de implementación concretas.
- Toda la lógica de negocio está separada del controlador para cumplir con principios de **Security by Design** y **Shift Left Security**.

---

## Resumen didáctico

Este ejemplo demuestra:

- Cómo aplicar validación robusta desde el modelo de dominio.
- Cómo controlar acceso a información sensible mediante roles definidos.
- Cómo documentar y proteger rutas de acuerdo con políticas mínimas de seguridad.
- La diferencia práctica frente a una API expuesta sin ningún requisito de seguridad.

---

**Uso exclusivo para fines académicos y como complemento de la versión insegura del Capítulo 2.**