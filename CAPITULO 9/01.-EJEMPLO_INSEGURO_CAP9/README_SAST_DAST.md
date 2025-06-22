# 🔍 Práctica SAST y DAST — Capítulo 9 (API REST Seguro)

## 📌 Objetivo

Este documento guía explica **paso a paso** cómo aplicar:

- ✅ **SAST** (Pruebas de Seguridad Estáticas): analizar el código fuente para detectar vulnerabilidades antes de la ejecución.
- ✅ **DAST** (Pruebas de Seguridad Dinámicas): evaluar la aplicación en ejecución para encontrar fallos de seguridad en tiempo real.

En este capítulo se compara **la versión insegura** y la **versión segura** para reforzar prácticas de auditoría profesional.

---

## ✅ 1️⃣ Requisitos previos

- 📂 Repositorio en **GitHub** o **GitLab** (público o privado).
- 💻 Proyecto funcional en local:  
  ```bash
  dotnet restore
  dotnet build
  dotnet run
  ```
- 🔧 Herramientas recomendadas:
  - **Roslyn Analyzers** (`dotnet format analyzers`)
  - **SonarQube** o **SonarCloud**
  - **GitHub CodeQL**
  - **OWASP ZAP** o **Burp Suite**

---

## ⚙️ 2️⃣ Estructura mínima de configuración

Tu repo debería incluir:

```plaintext
.
├── .github/
│   └── workflows/
│       └── codeql.yml   ✅ Análisis CodeQL en cada push
├── sonar-project.properties ✅ Configuración SonarQube
├── src/ ✅ Código fuente
├── README.md ✅ Instrucciones del proyecto
├── README_SAST_DAST.md ✅ Este archivo
```

---

## 🧩 3️⃣ Archivos de configuración

### 📄 A) `sonar-project.properties`

```properties
sonar.projectKey=NombreDeTuProyecto
sonar.organization=TuOrganizacion
sonar.host.url=https://sonarcloud.io
sonar.login=TOKEN_GENERADO

# Opcional: carpetas a analizar
sonar.sources=.
sonar.exclusions=**/bin/**,**/obj/**
```

👉 **Recuerda:** genera el **TOKEN** en SonarCloud o SonarQube.

### 📄 B) `.github/workflows/codeql.yml`

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

---

## 🚦 4️⃣ Ejecutar SAST en local y CI/CD

### ▶️ A) Local

1. **Roslyn Analyzers**  
   ```bash
   dotnet format analyzers
   ```

2. **SonarQube (local o SonarCloud)**  
   ```bash
   dotnet sonarscanner begin /k:"TuProyecto" /d:sonar.login="TOKEN"
   dotnet build
   dotnet sonarscanner end /d:sonar.login="TOKEN"
   ```

### ▶️ B) CI/CD

- Con **CodeQL**: cada `push` a `main` ejecuta análisis automáticamente (workflow anterior).
- Con **SonarCloud**: usar `sonar-project.properties` + Token de proyecto.

---

## 🕵️‍♂️ 5️⃣ Ejecutar DAST con ZAP

1. Ejecuta tu aplicación en local:  
   ```bash
   dotnet run
   ```
   Por defecto corre en `https://localhost:5001`.

2. Abre **OWASP ZAP**:
   - Configura proxy (opcional).
   - Agrega `https://localhost:5001` como objetivo.

3. Visita endpoints desde Postman o navegador:
   - `POST /api/account/register`
   - `POST /api/account/login`
   - `GET /api/account/profile`

4. En ZAP:
   - Ejecuta **Spider** para mapear rutas.
   - Corre un **Active Scan** para detectar problemas dinámicos.

---

## 📊 6️⃣ Comparar resultados

- ✅ **Versión Insegura:** Debe mostrar alertas (contraseñas en texto plano, falta de validación, CSRF).
- ✅ **Versión Segura:** Debe pasar SAST y DAST **sin hallazgos críticos**.

---

## 🏆 7️⃣ Resultado esperado

- 📌 Reportes de SonarQube: vulnerabilidades solo en versión insegura.
- 📌 Alertas de ZAP: no deben existir en versión segura.
- 📌 Pipeline de CI/CD con SAST corriendo en cada push.

---

## 🎓 8️⃣ Recomendación final

✔️ Guarda los reportes PDF o HTML generados.  
✔️ Inclúyelos como evidencia en tu carpeta de curso.  
✔️ Muestra la evolución de inseguro → seguro como parte de tu **cultura DevSecOps**.

---

## 🚀 Con esto cierras el Capítulo 9 con auditoría real, práctica y automatizada.