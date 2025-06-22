# NOTAS DEL EJEMPLO INSEGURO — CAPÍTULO 2

---

## Contexto

Este proyecto fue diseñado intencionalmente para demostrar lo que ocurre cuando un equipo **no realiza un levantamiento de requisitos de seguridad adecuado**. Sirve para contrastar directamente con los principios estudiados en la teoría del Capítulo 2.

---

## Principales omisiones detectadas

- **Falta de identificación de activos críticos**
  - Los reportes médicos contienen información altamente confidencial, pero no se clasificaron ni se definieron políticas de protección.

- **No se definieron actores y roles**
  - Cualquier persona, sin importar su nivel de autorización, puede crear o consultar reportes médicos.
  - No hay separación de roles (paciente, médico, auditor).

- **Endpoints expuestos sin restricciones**
  - Las rutas `/reportemedico/crear` y `/reportemedico/todos` están accesibles públicamente desde Internet, sin autenticación ni autorización.

- **Sin validación de datos de entrada**
  - El sistema acepta campos vacíos, datos incoherentes o manipulados.
  - No existe control de integridad ni restricciones de formato.

- **No se documentaron requisitos de seguridad**
  - No se definieron historias de abuso para anticipar ataques.
  - No se redactaron requisitos no funcionales para cifrado, auditoría o control de acceso.
  - No se incluyó ninguna política de cumplimiento normativo (por ejemplo, protección de datos de salud).

---

## Resumen didáctico

Este ejemplo muestra **cómo la ausencia de un levantamiento de requisitos de seguridad impacta directamente en la exposición de datos sensibles y la vulnerabilidad general del sistema**.

Sirve como base para reflexionar sobre:

- La importancia de clasificar y proteger activos desde la fase de análisis.
- El valor de definir claramente quién tiene derecho a realizar cada acción.
- La necesidad de mapear y reducir superficies de ataque expuestas.
- La obligación de documentar requisitos de seguridad junto con los funcionales.

---

**Este proyecto es exclusivo para fines educativos y NO debe utilizarse como base de producción.**