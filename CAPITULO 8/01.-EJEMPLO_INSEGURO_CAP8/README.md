# ✅ Ejemplo Inseguro — Capítulo 8

## 📌 Descripción

Este proyecto ilustra **malas prácticas en el manejo de errores en ASP.NET Core**, sirviendo como contraste directo con la versión segura:

- ❌ No se captura ninguna excepción de forma global (`UseExceptionHandler` ausente).
- ❌ No se aplica ningún middleware de fallback para controlar respuestas de error.
- ❌ Los **stack traces y mensajes internos** se exponen tal cual al cliente, revelando detalles de la estructura del código y configuraciones internas.

## ⚙️ Ejecución

Sigue estos pasos en la terminal:

```bash
dotnet restore
dotnet build
dotnet run
```

La aplicación estará disponible en:  
**http://localhost:5000**

## 🚩 Endpoints inseguros

- **GET** `/errorprueba/provocar`  
  Lanza una excepción no controlada.  
  *Resultado:* El cliente recibe todo el stack trace y el mensaje sin filtrado.

## ⚠️ Importante

Este ejemplo es **intencionalmente inseguro** para fines educativos.  
Permite comparar directamente con la versión **segura**, donde se aplican:

- Middleware global (`UseExceptionHandler`)
- Logging estructurado con `ILogger`
- Respuestas de error neutras (`ProblemDetails`)
- Configuración diferenciada según el ambiente (Desarrollo / Producción)

**🔑 Nota:**  
No se incluye una entidad funcional, ya que el objetivo es exclusivamente ilustrar el impacto de un manejo de errores deficiente. Se puede mantener una carpeta `Models` vacía o con un placeholder para coherencia de estructura.