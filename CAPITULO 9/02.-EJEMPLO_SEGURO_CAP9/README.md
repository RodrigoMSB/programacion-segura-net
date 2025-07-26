# ✅ Ejemplo SEGURO — Capítulo 9 (API REST)

## 📌 Descripción

Este proyecto es la **versión segura** del microservicio API REST para demostrar buenas prácticas de seguridad en .NET:

- ✔️ Validación de entrada en endpoints.
- ✔️ Hash de contraseñas con salt.
- ✔️ Sesión segura para mantener autenticación temporal.
- ✔️ Respuestas JSON, sin exponer datos sensibles.
- ❌ Sin vistas ni formularios web: todo se consume vía Postman o cliente HTTP.

---

## 🚀 Ejecución

```bash
dotnet restore
dotnet build
dotnet run
```

---

## 🔗 Endpoints Seguros

| Método | Ruta                   | Descripción                                         |
| ------ | ---------------------- | --------------------------------------------------- |
| POST   | `/api/account/register` | Registra usuario con contraseña hasheada.           |
| POST   | `/api/account/login`    | Verifica credenciales y establece sesión temporal.  |
| GET    | `/api/account/profile`  | Devuelve perfil autenticado (sin contraseña).       |

---

## ⚙️ Recomendación

- Prueba los endpoints con Postman usando JSON en cuerpo de solicitud.
- Revisa y ajusta configuración de sesión según tu entorno.
- Integra con SAST/DAST y pipelines CI/CD para auditoría continua.

---

## 📁 Importante

**Este ejemplo es intencionalmente didáctico:**  
No almacenar usuarios en memoria en producción. Implementar base de datos y autenticación robusta.

---
