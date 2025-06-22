# ✅ NOTAS DEL EJEMPLO INSEGURO — CAPÍTULO 7

## ⚠️ Descripción general

Este ejemplo demuestra una implementación intencionalmente defectuosa de manejo de sesiones para ilustrar riesgos frecuentes en aplicaciones web. Cada configuración y decisión muestra qué no se debe hacer y sirve como base para contrastar con la versión segura del capítulo.

## ❌ Puntos inseguros clave

- **Cookies de sesión sin banderas de seguridad**
  - `HttpOnly` deshabilitado: la cookie es accesible por JavaScript, exponiéndola a ataques XSS.
  - `SecurePolicy` en `None`: la cookie se envía por HTTP no cifrado, permitiendo sniffing.
  - `SameSite` en `None`: permite envío de cookies cross-site, facilitando ataques CSRF.

- **Sesión sin expiración adecuada**
  - `IdleTimeout` configurado en 365 días: sesión prácticamente indefinida, reutilizable tras cerrar navegador.

- **Logout inefectivo**
  - Solo limpia datos en servidor (`Session.Clear()`) pero no revoca la cookie en el cliente.
  - Resultado: la cookie de sesión sigue siendo válida y reutilizable.

- **Sin regeneración de ID de sesión**
  - No se regenera el `Session ID` tras autenticación exitosa, permitiendo `Session Fixation`.

- **Sin HTTPS ni encabezados de seguridad**
  - No aplica `HSTS` ni `UseHttpsRedirection`. Cookies viajan en texto claro.

## 🎓 Propósito didáctico

Este ejemplo NO DEBE USARSE en producción. Sirve para que el alumno identifique:

✔ Cómo la configuración débil facilita secuestro de sesión y CSRF.  
✔ Qué sucede si no se protege la cookie de sesión.  
✔ Cómo la mala gestión del ciclo de vida de la sesión permite accesos no autorizados.

## ✅ Comparación con la versión segura

| Aspecto | Versión Insegura | Versión Segura |
|---------|------------------|-----------------|
| **IdleTimeout** | 365 días | 15–30 minutos |
| **HttpOnly** | Desactivado | Activado |
| **SecurePolicy** | None | Always |
| **SameSite** | None | Strict o Lax |
| **Regeneración de ID** | No aplica | Sí, tras login |
| **Revocación de sesión** | Parcial | Completa |
| **HTTPS** | Opcional | Obligatorio con HSTS |

## 🚫 Advertencia

⚠️ Este ejemplo es SOLO para fines educativos. No replicar su configuración en proyectos reales.