# NOTAS DEL EJEMPLO INSEGURO — CAPÍTULO 5

---

## Propósito del Ejemplo

Este proyecto simula una **implementación deficiente de autenticación y autorización** para ilustrar de forma práctica **qué sucede cuando no se aplican controles de seguridad mínimos** en un sistema que expone endpoints sensibles.

El objetivo es permitir que los estudiantes identifiquen vulnerabilidades clásicas de malas prácticas:
- Ausencia de validación de credenciales.
- Ausencia de mecanismos de autenticación robustos como JWT.
- Roles asignados de forma arbitraria por el cliente.
- Acceso a recursos críticos sin ninguna verificación de identidad o privilegios.

Analizar este ejemplo inseguro es clave para entender **por qué se requiere un esquema robusto de autenticación y autorización**, tema que se refuerza en la versión segura de este capítulo.

---

## Problemas intencionales demostrados

- ❌ **Sin validación de credenciales:**  
  El endpoint de login no compara usuario ni contraseña con ningún origen de datos confiable.

- ❌ **Contraseñas en texto plano:**  
  Se permite enviar y almacenar contraseñas sin hashing ni cifrado.

- ❌ **Roles definidos por el cliente:**  
  El cliente puede incluir el rol 'Admin' directamente en la solicitud y el servidor lo acepta sin validación.

- ❌ **Sin tokens ni cookies seguras:**  
  No se usa JWT, ni cookies con HttpOnly o Secure. La sesión no se controla ni se firma.

- ❌ **Endpoints críticos sin restricción:**  
  Rutas que deberían estar limitadas a administradores son accesibles por cualquier usuario o incluso sin login previo.

---

## Contraste con la versión segura

En la versión segura del Capítulo 5 se aplican:
- ✅ **Autenticación robusta con JWT firmado y expiración.**
- ✅ **Validación de credenciales contra una fuente confiable.**
- ✅ **Asignación de roles controlada por el servidor, no por el cliente.**
- ✅ **Protección de recursos críticos usando `[Authorize(Roles = "...")]`.**
- ✅ **Respuesta clara y controlada ante accesos no autorizados.**

---

## Resumen Didáctico

Este ejemplo debe usarse **únicamente con fines educativos**, para mostrar de forma práctica **cómo una arquitectura mal diseñada facilita la escalación de privilegios y la exposición de información sensible**.

Estudiar este caso permite reforzar la importancia de aplicar patrones de autenticación y autorización seguros en cualquier aplicación moderna.

---

**Material exclusivo para formación en prácticas seguras de desarrollo de software.**
