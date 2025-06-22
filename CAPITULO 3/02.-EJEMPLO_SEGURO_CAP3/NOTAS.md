# NOTAS DEL EJEMPLO SEGURO — CAPÍTULO 3

---

## Contexto

Este proyecto demuestra cómo aplicar **de forma práctica** los principios de diseño y arquitectura segura estudiados en el Capítulo 3.  
Sirve como contraste directo con el ejemplo inseguro, evidenciando cómo una arquitectura correctamente planificada mitiga vulnerabilidades comunes.

---

## Principales prácticas aplicadas

- **Separación de responsabilidades**
  - La lógica de negocio se encapsula dentro de `UsuarioService`.  
  - El controlador se limita a orquestar peticiones y aplicar validación de modelo.
  - Esta separación permite aislar cambios, testear en forma modular y minimizar puntos de fallo.

- **Validación de entrada robusta**
  - El modelo `Usuario` aplica atributos `[Required]`, `[StringLength]` y `[RegularExpression]` para garantizar que solo datos válidos entren a la lógica del sistema.
  - `ModelState` verifica la validez antes de persistir información.

- **Control de acceso y roles**
  - Se simula un control de roles usando el header `rol`.  
  - Acciones críticas, como acceder al secreto de la API, requieren explícitamente el rol `Admin`.
  - Ilustra cómo aplicar el principio de **mínimo privilegio** en la práctica.

- **Gestión segura de secretos**
  - La clave de API no está hardcodeada: se recupera desde una variable de entorno.
  - Esto refleja una práctica básica de **protección de configuración sensible**.

- **Protección de la comunicación**
  - La aplicación fuerza HTTPS para cifrar todo el tráfico entre cliente y servidor.

- **Logging estructurado**
  - Se habilita logging por consola para auditar operaciones, facilitando la detección de accesos y comportamientos inesperados.

---

## Resumen didáctico

Este ejemplo integra todos los conceptos clave del **Capítulo 3**:
- **Separación de responsabilidades**
- **Principio de menor privilegio**
- **Defensa en profundidad**
- **Protección de configuraciones sensibles**

Permite a los alumnos ver cómo cada principio se refleja en una decisión de diseño, línea de código o patrón arquitectónico.  
Sirve como plantilla base para proyectos reales y como referencia para implementar seguridad en soluciones empresariales basadas en **.NET Core**.

---

**Uso exclusivo para fines académicos y como complemento del ejemplo inseguro del Capítulo 3.**