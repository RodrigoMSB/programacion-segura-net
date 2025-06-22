# ✅ Práctica SAST y DAST — Capítulo 9 (Micro Front-End SEGURO)

## 📌 Objetivo

Este archivo acompaña la **versión SEGURA** del micro front-end, demostrando cómo aplicar **Pruebas de Seguridad Estáticas (SAST)** y **Pruebas de Seguridad Dinámicas (DAST)** para validar que las vulnerabilidades encontradas en la versión insegura han sido correctamente mitigadas.

---

## ✅ Requisitos previos

- Repositorio en **GitHub** o **GitLab** (idealmente privado).
- Aplicación funcional local (`dotnet run`).
- Herramientas sugeridas:
  - **Roslyn Analyzers** (`dotnet format analyzers`)
  - **SonarQube** (local o SonarCloud)
  - **GitHub CodeQL**
  - **OWASP ZAP** o **Burp Suite**

---

## 🚦 1️⃣ Ejecutar SAST en CI/CD

### ▶️ Con SonarQube:

1. Instala **SonarQube** local o usa **SonarCloud**.
2. Agrega archivo `sonar-project.properties`.
3. Genera token del proyecto en la UI.
4. Ejecuta:
   ```bash
   dotnet sonarscanner begin /k:"TuProyectoSeguro" /d:sonar.login="TOKEN"
   dotnet build
   dotnet sonarscanner end /d:sonar.login="TOKEN"
   ```
5. Verifica resultados en Sonar.

### ▶️ Con GitHub CodeQL:

1. Activa **GitHub Advanced Security** en tu repo.
2. Añade workflow `codeql.yml`:
   ```yaml
   name: CodeQL Analysis

   on:
     push:
       branches: [ main ]

   jobs:
     analyze:
       name: Analyze
       runs-on: ubuntu-latest
       steps:
         - uses: actions/checkout@v3
         - name: Initialize CodeQL
           uses: github/codeql-action/init@v2
           with:
             languages: csharp
         - name: Autobuild
           uses: github/codeql-action/autobuild@v2
         - name: Perform CodeQL Analysis
           uses: github/codeql-action/analyze@v2
   ```

3. Cada `push` correrá el análisis SAST automáticamente.

---

## 🕵️‍♂️ 2️⃣ Ejecutar DAST con ZAP

1. Corre la app segura (`dotnet run`).
2. Abre **OWASP ZAP** y configura el proxy.
3. Accede a los endpoints REST:
   - `/api/account/register`
   - `/api/account/login`
   - `/api/account/profile`
4. Usa **Spider** de ZAP para mapear rutas.
5. Ejecuta escaneo activo para verificar que no haya inyecciones, CSRF, o filtraciones.

---

## 📊 3️⃣ Comparar resultados

- Los reportes de la versión **segura** deben evidenciar **0 hallazgos críticos**.
- Sirve como prueba de remediación frente a la versión insegura.

---

## 🎓 Recomendación

Guarda ambos reportes y preséntalos como evidencia en clases, auditorías internas o flujos **DevSecOps** para demostrar la diferencia entre un software expuesto y uno protegido.

---

## ✅ Resultado esperado

- ✔️ SAST limpio.
- ✔️ DAST sin vulnerabilidades.
- ✔️ Confianza en la robustez del micro front-end seguro.



# 📁 Estructura de Archivos — Configuración SAST/DAST

Esta sección describe dónde colocar los archivos clave para integrar SAST y DAST en tu proyecto seguro del Capítulo 9.

## ✅ Estructura recomendada

```
/TuProyecto/
├── sonar-project.properties
├── .github/
│   └── workflows/
│       └── codeql.yml
├── EjemploSeguroCapitulo9.csproj
├── Program.cs
├── Controllers/
├── Services/
├── ...
```

## 📌 Descripción

- **sonar-project.properties** → Configura SonarQube para análisis estático local o CI/CD.
- **.github/workflows/codeql.yml** → Define pipeline de GitHub Actions para ejecutar análisis SAST con CodeQL automáticamente en cada push.

Con estos archivos, tu repositorio queda listo para aplicar auditorías de seguridad tanto locales como en pipeline CI/CD.
