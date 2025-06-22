# ✅ Ejemplo SEGURO — Capítulo 8

## 📌 Descripción

Este proyecto demuestra cómo implementar un **manejo de errores centralizado y seguro** en ASP.NET Core, siguiendo buenas prácticas de desarrollo profesional:

- ✅ **Captura global de excepciones:** usando `UseExceptionHandler` para interceptar y manejar todos los errores no controlados.
- ✅ **Registro estructurado:** con `ILogger`, cada excepción se registra con `TraceId` para facilitar la auditoría y el soporte técnico.
- ✅ **Respuesta neutra al cliente:** se devuelve un objeto `ProblemDetails` conforme a RFC 7807, ocultando detalles internos del servidor.

## ⚙️ Ejecución

Ejecuta estos comandos en la terminal desde la carpeta raíz del proyecto:

```bash
dotnet restore
dotnet build
dotnet run
```

Por defecto, la aplicación escuchará en:
**http://localhost:5000**

## 🚩 Endpoints de prueba

- **GET** `/prueba/provocar`  
  Lanza una excepción intencionalmente.  
  ✅ Resultado: la excepción es capturada de forma centralizada, registrada con `ILogger` y devuelta como un mensaje genérico seguro.

## ✅ Importante

Este ejemplo ilustra claramente cómo proteger información sensible y cómo registrar errores de forma adecuada, contrastando directamente con la versión insegura del mismo capítulo.

**🔑 Tip:**  
No se requiere una entidad de dominio funcional ya que el foco es el pipeline de manejo de errores. Se mantiene la arquitectura con `Controllers` y `Program.cs` para coherencia pedagógica.