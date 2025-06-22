
# NOTAS IMPORTANTES PARA USO DEL PROYECTO

## Entorno y Swagger

Por diseño, Swagger (documentación interactiva de la API) y la página de errores detallados solo se activan cuando el proyecto se ejecuta en entorno `Development`.

Cuando se ejecuta desde terminal sin configuración extra, ASP.NET Core asume el entorno `Production` y no mostrará Swagger.

## Cómo activar el entorno `Development`

### En macOS o Linux

```bash
export ASPNETCORE_ENVIRONMENT=Development
dotnet run
```

### En Windows PowerShell

```powershell
setx ASPNETCORE_ENVIRONMENT "Development"
dotnet run
```

## Recomendación

Para entornos reales de producción, mantenga el entorno como `Production` para mayor seguridad.
