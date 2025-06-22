# 🚫 Ejemplo Inseguro — Despliegue y Mantenimiento

## 📌 Descripción
Este proyecto demuestra malas prácticas intencionales en:
- Código fuente (credenciales hardcodeadas).
- Dockerfile (sin versión base ni usuario no privilegiado).
- Pipeline CI/CD (sin firma, sin escaneo, secrets expuestos).

Se usa como base para contrastar con la versión segura.

## 📁 Contenido
- `Program.cs` — App mínima con cadena de conexión insegura.
- `appsettings.json` — Config con credenciales planas.
- `Dockerfile` — Construcción insegura.
- `pipeline_inseguro.yml` — Flujo CI/CD con malas prácticas.

## ▶️ Cómo probar rápido
```bash
dotnet restore
dotnet build
dotnet run
```
Verás la cadena de conexión expuesta en consola.

## 🐳 Cómo construir imagen (opcional)
```bash
docker build -t myinsecureapp .
docker run -p 8080:80 myinsecureapp
```

## ⚠️ Atención
- Este código NO DEBE usarse en producción.
- Su objetivo es mostrar fallos comunes para luego corregirlos en la versión segura.

## ✅ Siguiente paso
1. Subir este ejemplo a GitHub.
2. Ejecutar `pipeline_inseguro.yml` vía GitHub Actions.
3. Revisar los logs y evidenciar los riesgos.
4. Comparar con el ejemplo SEGURO.
