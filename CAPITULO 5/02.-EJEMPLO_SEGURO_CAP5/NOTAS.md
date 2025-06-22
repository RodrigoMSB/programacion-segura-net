# NOTAS DEL EJEMPLO SEGURO — CAPÍTULO 5

---

## Descripción General

Este proyecto muestra una **implementación correcta de autenticación y autorización** en una API REST moderna utilizando **JSON Web Tokens (JWT)** y control de acceso basado en roles.

Sirve como contraste directo con la versión insegura, demostrando cómo aplicar prácticas sólidas para proteger credenciales, restringir rutas críticas y controlar permisos de forma segura.

---

## Buenas Prácticas Aplicadas

- ✅ **Validación de credenciales:**  
  El servidor valida usuario y contraseña antes de generar un token. Solo usuarios legítimos reciben un JWT.

- ✅ **Emisión de JWT firmado:**  
  El token incluye **claims** con información de identidad y rol del usuario. Se firma con una clave secreta, evitando manipulación.

- ✅ **Protección de rutas sensibles:**  
  Se usa `[Authorize(Roles = "Admin")]` para restringir acceso a rutas que requieren privilegios elevados.

- ✅ **Verificación automática de token:**  
  El middleware de autenticación comprueba la firma, la expiración y los claims en cada solicitud.

- ✅ **Separación de autenticación y autorización:**  
  Se ilustra claramente la diferencia: autenticación prueba la identidad; autorización define lo que puede hacer el usuario autenticado.

---

## Contraste con la versión insegura

| Aspecto | Versión Insegura | Versión Segura |
| ------- | ---------------- | ---------------- |
| Validación de credenciales | ❌ No existe | ✅ Comparación real |
| Generación de token | ❌ No hay | ✅ JWT firmado |
| Control de roles | ❌ El cliente envía cualquier rol | ✅ Rol embebido en el token, verificado |
| Protección de endpoints | ❌ Todos expuestos | ✅ Restricción por `[Authorize]` y claims |
| Clave secreta | ❌ Inexistente | ✅ Usada para firmar y validar JWT |

---

## Resumen

Este ejemplo refuerza los conceptos clave para una arquitectura robusta:
- **Validar siempre la identidad antes de emitir tokens.**
- **Firmar y verificar los JWT correctamente.**
- **Aplicar control de acceso granular usando roles y políticas.**
- **Proteger claves secretas y usarlas de forma segura.**

---

**Uso exclusivo para prácticas educativas en desarrollo seguro de software.**