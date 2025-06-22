# NOTAS DEL EJEMPLO SEGURO — CAPÍTULO 2

---

## Contexto

Este proyecto ejemplifica cómo un **levantamiento de requisitos de seguridad bien ejecutado** influye directamente en el diseño técnico y en la robustez de la API.

Se utiliza el mismo escenario de gestión de reportes médicos que el ejemplo inseguro, pero aplicando principios fundamentales desde la fase de análisis de requisitos.

---

## Principales prácticas aplicadas

- **Identificación y protección de activos sensibles**
  - Los campos del modelo `ReporteMedico` se validan mediante anotaciones (`[Required]`, `[StringLength]`), limitando entradas erróneas o abusivas.
  - Los datos confidenciales no quedan expuestos sin control.

- **Definición de actores y roles**
  - Se implementa un control de rol simulado: solo usuarios con rol **Médico** pueden consultar todos los reportes.
  - Este control de acceso ejemplifica cómo se define quién puede acceder a cada activo.

- **Validación robusta de datos**
  - El modelo asegura requisitos no funcionales de seguridad a nivel de estructura de datos.
  - El controlador valida `ModelState` antes de persistir información.

- **Separación de responsabilidades**
  - Se implementa una **capa de servicio** (`ReporteMedicoService`) para manejar la lógica de negocio.
  - El controlador se limita a orquestar peticiones, alineándose a principios de arquitectura limpia.

- **Superficie de ataque reducida**
  - Endpoints críticos como la consulta de todos los reportes requieren autenticación por rol.
  - Accesos no autorizados son bloqueados y se devuelve un error claro.

---

## Resumen didáctico

Este ejemplo demuestra cómo **un levantamiento de requisitos de seguridad sólido** permite:

- Anticipar quién usará el sistema y qué permisos necesita.
- Proteger activos de alto valor con validación y control de acceso.
- Documentar y aplicar políticas mínimas de cumplimiento.
- Reducir la superficie de ataque desde el diseño, cumpliendo con el enfoque de *Security by Design* y *Shift Left Security*.

---

**Este proyecto es exclusivamente para fines educativos y como referencia práctica del Capítulo 2.**
