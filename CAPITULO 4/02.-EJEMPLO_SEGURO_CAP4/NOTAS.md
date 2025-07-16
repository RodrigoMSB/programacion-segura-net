# NOTAS DEL EJEMPLO SEGURO — CAPÍTULO 4

---

## Descripción General

Este proyecto muestra cómo aplicar múltiples capas de validación para proteger un formulario de registro.  
Combina herramientas propias de .NET Core con una librería externa para garantizar que **sólo datos válidos y coherentes** lleguen a la lógica de negocio.

---

## Prácticas clave aplicadas

- ✅ **Data Annotations para reglas básicas:**
  Se definen atributos declarativos directamente en el modelo de dominio (`UsuarioRegistro`), por ejemplo:
  `[Required]`, `[StringLength]`, `[EmailAddress]`.

- ✅ **FluentValidation para validación avanzada:**
  Se desacoplan reglas complejas en un validador específico (`UsuarioRegistroValidator`).  
  Ejemplo: verificar complejidad de la contraseña (mayúsculas, minúsculas, números).

- ✅ **Validación de `ModelState` en el controlador:**
  Antes de invocar cualquier lógica de negocio, el controlador comprueba si todos los datos cumplen con las reglas.

- ✅ **Rechazo de entradas inválidas:**
  Si los datos no cumplen las reglas, la API responde con un `400 Bad Request` y detalles claros de los errores.

- ✅ **Mensajes de error estructurados:**
  La respuesta explica exactamente qué campos fallaron y por qué, sin exponer información interna o traza técnica.

---

## Contraste con la versión insegura

| Aspecto | Versión Insegura | Versión Segura |
|---------|------------------|-----------------|
| Validación básica | ❌ Ausente | ✅ Data Annotations |
| Validación avanzada | ❌ Ausente | ✅ FluentValidation |
| ModelState | ❌ No se usa | ✅ Se usa |
| Respuesta de error | ❌ No estructurada | ✅ Estructurada |
| Control de longitud/formato | ❌ Ninguno | ✅ Comprobado |

---

## Recomendación

Analizar este ejemplo junto al **Ejemplo Inseguro — Capítulo 4** permite entender la importancia de aplicar validación en múltiples capas, reutilizar validadores y mantener la API protegida contra entradas maliciosas.

---

**Uso exclusivo para fines académicos y de capacitación en seguridad de software.**