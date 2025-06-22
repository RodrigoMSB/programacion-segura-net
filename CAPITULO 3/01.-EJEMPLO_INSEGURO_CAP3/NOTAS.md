# NOTAS DEL EJEMPLO INSEGURO — CAPÍTULO 3

---

## Contexto

Este proyecto intencionalmente muestra cómo se ve una **arquitectura mal diseñada en términos de seguridad**. 
Sirve para contrastar y debatir cada principio de diseño seguro abordado en el Capítulo 3.

---

## Principales vulnerabilidades y malas prácticas

- **Claves y secretos expuestos:**  
  Se incrustan valores sensibles directamente en el código fuente (`API_KEY_SUPER_SECRETA_123`), 
  violando principios de protección de configuración y mínima exposición.

- **Cualquiera puede asumir rol de Administrador:**  
  El modelo `Usuario` permite que cualquier registro se autodeclare como `Admin` sin verificación ni restricción, 
  quebrantando el principio de menor privilegio.

- **Ausencia de validación de entrada:**  
  Los controladores aceptan cualquier dato sin comprobaciones de formato, longitud o consistencia. 
  No se usa `ModelState` ni atributos de validación.

- **Sin separación de responsabilidades:**  
  Toda la lógica de negocio y acceso a datos se encuentra mezclada en el controlador, 
  incumpliendo el principio de separación de capas.

- **Falta de autenticación y autorización:**  
  No existen roles reales, claims ni políticas para controlar el acceso a información sensible. 
  Cualquiera puede acceder a todos los datos o secretos.

---

## Resumen didáctico

Este ejemplo sintetiza **anti-patrones de arquitectura segura** y expone claramente qué sucede 
cuando se ignoran prácticas como la defensa en profundidad y el diseño por capas. 
Su análisis permite reforzar la importancia de aplicar desde el inicio principios como:
- Separación de responsabilidades
- Mínimo privilegio
- Protección de configuraciones
- Control de acceso y validación de roles

---

**Este proyecto es exclusivamente para fines académicos y de contraste con la versión segura del Capítulo 3.**