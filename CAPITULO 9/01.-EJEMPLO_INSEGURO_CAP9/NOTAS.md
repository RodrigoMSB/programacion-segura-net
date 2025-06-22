# 🐞 NOTAS DEL EJEMPLO INSEGURO — CAPÍTULO 9 (Micro Front-End)

Este ejemplo fue diseñado intencionalmente para evidenciar **malas prácticas de seguridad** comunes en aplicaciones web con interfaz gráfica y para practicar técnicas de análisis estático (**SAST**) y dinámico (**DAST**).

## 📌 Puntos clave de vulnerabilidad

- **❌ Formularios sin validación de lado servidor ni sanitización:**  
  Los campos `username` y `password` se procesan sin restricción, permitiendo inyecciones de scripts y datos maliciosos.

- **❌ Contraseñas almacenadas sin hash ni cifrado:**  
  Las credenciales se guardan como texto plano en `TempData`, facilitando filtraciones y ataques de fuerza bruta.

- **❌ Ausencia de tokens CSRF:**  
  Los formularios no utilizan `@Html.AntiForgeryToken()`, lo que deja abierta la puerta a ataques CSRF triviales.

- **❌ Simulación de autenticación con TempData:**  
  Se usa `TempData` para retener usuario y contraseña entre vistas, lo cual no es persistente ni seguro.

- **❌ Exposición directa de información sensible:**  
  La página de perfil muestra en texto claro tanto el usuario como la contraseña.

- **⚠️ Advertencias de compilación intencionales:**  
  Se generan advertencias relacionadas con `AntiForgeryToken` mal invocado y falta de `wwwroot`. Esto refuerza que no se implementa protección CSRF ni manejo adecuado de archivos estáticos.

## 🎯 Práctica recomendada de SAST y DAST

✅ **Para completar la práctica de este capítulo:**

1️⃣ **Sube este proyecto a un repositorio GitHub privado o GitLab.**  
2️⃣ Configura un pipeline CI/CD con herramientas como **SonarQube**, **CodeQL** o **Roslyn Analyzers**.  
   - Cada push debe gatillar el análisis SAST para detectar vulnerabilidades de código.
3️⃣ Ejecuta un escaneo **DAST** usando **OWASP ZAP** o **Burp Suite** mientras navegas los formularios de registro, login y perfil.
4️⃣ Compara los hallazgos con los reportes de la **versión segura** para comprobar la mitigación de riesgos.

## 🎓 Contexto didáctico

Este micro front-end refuerza cómo aplicar seguridad desde el diseño y el ciclo de vida del desarrollo, mostrando el contraste entre un sistema vulnerable y uno corregido mediante controles reales.

---

✅ **Nota:**  
Todos estos problemas se resuelven en la **versión segura**, que incluye:
- Hashing de contraseñas.
- Validación y sanitización de entradas.
- Tokens antiforgery CSRF.
- Sesiones correctamente gestionadas.
- Encabezados y cookies configuradas para producción.

---

📁 **Este proyecto NO debe usarse como base de producción. Solo para prácticas de auditoría y formación profesional.**