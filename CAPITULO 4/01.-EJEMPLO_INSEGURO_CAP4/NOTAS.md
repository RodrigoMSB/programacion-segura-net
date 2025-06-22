# NOTAS DEL EJEMPLO INSEGURO — CAPÍTULO 4

---

## Descripción General

Este proyecto muestra cómo la falta de validación de entradas puede dejar expuesta cualquier API o formulario web.  
Aquí se omiten deliberadamente todas las prácticas de validación y sanitización para evidenciar los riesgos.

---

## Principales malas prácticas demostradas

- **Sin validación del lado servidor:**  
  El formulario de registro procesa los datos tal como se reciben, sin verificar requisitos de formato, longitud ni coherencia.

- **Sin Data Annotations ni FluentValidation:**  
  El modelo no usa ningún atributo de validación y no se integra un validador externo para reglas más complejas.

- **Entradas sin restricción de longitud o contenido:**  
  Campos como `Nombre`, `Email` o `Contrasena` pueden contener scripts, cadenas maliciosas o datos excesivamente largos.

- **Exposición a ataques comunes:**  
  El sistema es vulnerable a Cross-Site Scripting (XSS) y SQL Injection (en caso de persistencia) porque confía ciegamente en el input.

- **Manejo de errores inexistente:**  
  No hay control de excepciones ni mensajes de error estructurados; la respuesta se genera directamente con los datos del usuario.

---

## Contraste con la versión segura

En la versión segura del Capítulo 4 se aplicarán:
- **Validación robusta:** usando Data Annotations para reglas básicas y FluentValidation para validación avanzada.
- **Verificación en el servidor:** con `ModelState.IsValid` antes de procesar cualquier dato.
- **Sanitización y protección:** para bloquear entradas potencialmente maliciosas.
- **Manejo seguro de errores:** devolviendo respuestas claras pero sin exponer detalles internos de la lógica.

---

## Recomendación

Analizar línea por línea este ejemplo para entender cómo pequeños descuidos en validación abren la puerta a ataques comunes.  
Compararlo con la versión segura permitirá reforzar principios de **programación defensiva**, **validación de entrada múltiple** y **mínima superficie de ataque**.

---

**Este proyecto NO debe usarse en ambientes de producción. Es material exclusivo para fines educativos.**