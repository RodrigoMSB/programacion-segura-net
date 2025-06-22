# ✅ NOTAS DEL EJEMPLO INSEGURO — CAPÍTULO 8

## 🔍 Aspectos inseguros demostrados

- ❌ **Sin middleware global:**  
  No usa `UseExceptionHandler` para capturar y manejar excepciones de forma centralizada.

- ❌ **Propagación directa de excepciones:**  
  Cualquier error lanzado se muestra tal cual al cliente, incluyendo el **stack trace** completo, lo que expone detalles internos de la aplicación y la estructura del código.

- ❌ **Sin logging estructurado:**  
  No existe registro interno de las excepciones, lo que dificulta la auditoría, el monitoreo y la resolución de problemas.

- ❌ **Información sensible expuesta:**  
  Permite que rutas, nombres de métodos y detalles de configuración sean visibles a usuarios y posibles atacantes.

## 📂 Arquitectura del ejemplo

- **Entidad de dominio:**  
  👉 *No se incluye una entidad funcional real porque este ejemplo se enfoca **exclusivamente** en ilustrar el impacto de un manejo de errores inadecuado.*  
  👉 Se omite o se puede agregar un **placeholder `Usuario`** únicamente para mantener coherencia en la estructura de carpetas `Models` en todo el curso.

## Propósito didáctico

Este ejemplo sirve como **contrapunto directo** a la versión **segura**, permitiendo a los estudiantes observar:
- Cómo un error mal manejado puede filtrar información sensible.
- La importancia de registrar errores internamente sin exponer detalles al usuario.
- La necesidad de diferenciar la presentación de errores según el ambiente (Desarrollo vs Producción).