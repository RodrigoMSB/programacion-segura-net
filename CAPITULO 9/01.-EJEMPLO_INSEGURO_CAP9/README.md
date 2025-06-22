# ⚠️ Ejemplo Inseguro — Capítulo 9 (Micro Front-End)

## 📌 Descripción

Este proyecto es una **mini aplicación ASP.NET Core MVC insegura**, construida con fines **exclusivamente educativos**, para practicar técnicas de análisis de seguridad (SAST y DAST):

- Simula un flujo real de **registro, login y perfil** con prácticas vulnerables.
- Expone datos sensibles y sin protección intencionalmente.

## 🚀 Ejecución

Para correr el proyecto en local:

```bash
dotnet restore
dotnet build
dotnet run
```

Accede luego a:

```
http://localhost:5000
```

## 🧭 Cómo probar la aplicación (⚠️ Usar navegador, NO Postman)

1️⃣ Abre el navegador y visita:

| Página        | URL                                                                              |
| ------------- | -------------------------------------------------------------------------------- |
| **Registro:** | [http://localhost:5000/Account/Register](http://localhost:5000/Account/Register) |
| **Login:**    | [http://localhost:5000/Account/Login](http://localhost:5000/Account/Login)       |
| **Perfil:**   | [http://localhost:5000/Account/Profile](http://localhost:5000/Account/Profile)   |

2️⃣ Completa **Usuario** y **Contraseña** en los formularios.

3️⃣ Envía el formulario y observa cómo se muestran datos sensibles sin protección.

## 🔓 Endpoints inseguros

| Método | Ruta                | Descripción                                 |
| ------ | ------------------- | ------------------------------------------- |
| `GET`  | `/Account/Register` | Formulario de registro inseguro.            |
| `POST` | `/Account/Register` | Registra usuario sin validación ni cifrado. |
| `GET`  | `/Account/Login`    | Formulario de login inseguro.               |
| `POST` | `/Account/Login`    | Login sin autenticación real.               |
| `GET`  | `/Account/Profile`  | Muestra perfil con datos sensibles.         |

## ⚠️ Importante

> **Este ejemplo es deliberadamente inseguro.**\
> No usar en producción.\
> Se recomienda subir este proyecto a un **repositorio GitHub o GitLab privado** para ejecutar análisis SAST y DAST como parte de un pipeline CI/CD real.

## ✅ Próximos pasos sugeridos

- Subir a un repo y configurar un pipeline con **Roslyn Analyzers**, **SonarQube** o **CodeQL**.
- Escanear dinámicamente con **OWASP ZAP** o **Burp Suite** mientras se navega el front-end.
- Comparar resultados con la versión segura para validar la mitigación de vulnerabilidades.

## 🗂️ Notas

- No incluye antiforgery ni hashing.
- Revisa `NOTAS.md` para ver el resumen de vulnerabilidades y recomendaciones.