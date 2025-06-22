# NOTAS DEL EJEMPLO INSEGURO

---

## Propósito de este ejemplo

Este proyecto muestra deliberadamente **cómo NO debe desarrollarse un módulo de registro e inicio de sesión** en una aplicación web moderna.  
Sirve como base de comparación para identificar malas prácticas y entender cómo aplicar seguridad y principios de arquitectura limpia correctamente.

---

## Principales malas prácticas implementadas

### 1. Contraseñas almacenadas en texto plano
- La contraseña se guarda tal cual llega del cliente, sin cifrado ni hash.
- Riesgo directo de exposición en caso de fuga de base de datos.

**Solución recomendada:**  
Aplicar hash robusto (por ejemplo `BCrypt`) y nunca guardar contraseñas reversibles.

---

### 2. Sin validación de datos de entrada
- El modelo `Usuario` no tiene validaciones: acepta campos vacíos o datos basura.
- Permite crear múltiples usuarios con el mismo nombre de usuario.

**Solución recomendada:**  
Usar anotaciones de validación (`[Required]`, `[StringLength]`) y validaciones adicionales en la capa de servicio.

---

### 3. Autenticación inexistente
- No se usa JWT, cookies seguras ni sesiones.
- Cualquiera puede acceder a rutas sensibles como `/perfil` sin autenticarse.

**Solución recomendada:**  
Implementar autenticación con JWT o ASP.NET Core Identity y proteger rutas con `[Authorize]`.

---

### 4. Acceso directo al contexto de base de datos
- El controlador usa directamente `ContextoBaseDatos`.
- No hay capa de servicio ni patrón repositorio.

**Solución recomendada:**  
Separar responsabilidades:  
- **Controlador:** solo orquesta peticiones y respuestas.  
- **Servicio:** aplica reglas de negocio y validaciones.  
- **Repositorio:** interactúa con la base de datos.

---

### 5. Sin manejo de errores ni logs
- No hay control de excepciones.
- No se registran eventos de seguridad ni intentos fallidos de login.

**Solución recomendada:**  
Agregar `Middleware` de manejo global de excepciones y logs de auditoría.

---

### 6. Configuración de base de datos hardcodeada
- El nombre de la base de datos en memoria se define dentro de `Program.cs`.
- No se usa `appsettings.json`.

**Solución recomendada:**  
Centralizar configuraciones en `appsettings.json` o variables de entorno.

---

### 7. Nomenclatura mixta (carpetas en inglés, clases en español)
- En este proyecto se hace así para respetar las convenciones .NET y mantener la comprensión didáctica.
- En producción se recomienda una nomenclatura coherente y consistente en todo el proyecto.

---

## Resumen didáctico

Este ejemplo inseguro permite:
- Identificar vulnerabilidades reales en el código.
- Auditar la seguridad básica de una API.
- Contrastar con la versión segura, que aplica principios de arquitectura limpia y buenas prácticas de desarrollo.
- Practicar mejoras incrementales: validación, hashing, autenticación, control de acceso, logging.

---

## Próximos pasos

1. Ejecutar la versión **segura** del mismo proyecto.
2. Analizar cada mejora aplicada y entender cómo mitiga vulnerabilidades.
3. Implementar prácticas equivalentes en proyectos reales.

 
