
# ✅ NOTAS DEL EJEMPLO SEGURO — Capítulo 9 (Micro Front-End + API REST)

## 📌 Medidas de seguridad implementadas

- ✔️ **Validación de entrada:**  
  Todos los formularios y endpoints validan que los campos `Nombre` y `Password` no sean vacíos o nulos, previniendo inputs maliciosos o credenciales incompletas.

- ✔️ **Hash de contraseñas:**  
  Las contraseñas no se almacenan en texto plano. Se utiliza hashing con salt aleatorio y algoritmo PBKDF2 (100.000 iteraciones con SHA-256) para proteger credenciales incluso ante brechas de base de datos.

- ✔️ **Antiforgery Tokens:**  
  Para formularios web (si se usa interfaz web complementaria), se incluyen `@Html.AntiForgeryToken()` para mitigar ataques CSRF. En la API REST, se omite por ser consumo JSON puro (se recomienda token JWT o Header custom si se extiende).

- ✔️ **Sesión segura:**  
  No se usa `TempData` para contraseñas ni datos sensibles. La autenticación se mantiene mediante `Session` gestionada por `ISession` con cookies seguras, sin exponer tokens o secretos en la URL.

- ✔️ **No se expone información sensible:**  
  El endpoint de perfil (`/api/account/profile`) y la vista de perfil (si aplica) solo muestran el nombre de usuario autenticado. Nunca se devuelve ni el hash ni la contraseña original.

- ✔️ **Sin archivos estáticos innecesarios:**  
  Para evitar vectores de ataque de recursos expuestos, se limita la exposición a solo controladores y rutas necesarias.

- ✔️ **Pipeline de CI/CD recomendado:**  
  Este proyecto es apto para integrarse en pipelines automáticos, ejecutar escaneos **SAST** (Roslyn Analyzers, SonarQube, CodeQL) y pruebas dinámicas **DAST** (OWASP ZAP, Burp Suite).

## 🔑 Contexto

Este micro front-end + API REST sirve como base de prácticas para:
- Demostrar **buenas prácticas de autenticación y gestión de sesiones**.
- Preparar a equipos para identificar fallos comunes usando herramientas de análisis de seguridad.
- Ser un esqueleto para integrar autenticación robusta (JWT, OAuth) si se amplía a aplicaciones de mayor escala.

> **Importante:** Este ejemplo NO usa persistencia de base de datos ni usuarios reales: todos los registros viven en memoria. Para entornos reales, se debe conectar a una base de datos segura y cifrada.