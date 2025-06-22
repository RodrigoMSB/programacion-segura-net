# Ejemplo Inseguro — Capítulo 1

## Descripción general

Este proyecto forma parte del Capítulo 1 del curso de Seguridad y Buenas Prácticas en Aplicaciones .NET.  
Su propósito es exponer una implementación deliberadamente insegura, para analizar riesgos reales y contrastarlos con la versión segura.

Se mantienen carpetas y namespaces en inglés, siguiendo las convenciones de .NET, pero las clases, propiedades y comentarios están redactados en español para facilitar la comprensión didáctica.

---

## Propósito didáctico

Este ejemplo permite:

- Identificar malas prácticas de seguridad en aplicaciones web.
- Auditar vulnerabilidades reales.
- Contrastar con una versión segura y profesional.
- Realizar pruebas prácticas mediante Postman, observando cómo se comporta una API sin validación ni control de acceso.

---

## Principales características inseguras

- Contraseñas almacenadas en texto plano.
- Sin validación de datos de entrada.
- Sin autenticación ni autorización real.
- Acceso abierto a rutas que deberían estar protegidas.
- Contexto de base de datos utilizado directamente desde el controlador.
- Configuración hardcodeada en `Program.cs`.
- Sin manejo de errores centralizado ni logs de seguridad.

---

## Estructura del proyecto

```
01.-EJEMPLO_INSEGURO_CAP1/
├── Controllers/
│   └── CuentaController.cs
├── Models/
│   └── Usuario.cs
├── Program.cs
├── appsettings.json
├── launchSettings.json
├── NOTAS.md
├── README.md
├── .gitignore
├── EjemploInseguroCapitulo1.csproj
```

---

## Requisitos

- Tener instalado el SDK de .NET 8
- Cliente HTTP (Postman o similar)

---

## Cómo ejecutar

1. Abre una terminal en la raíz del proyecto.

2. Restaura dependencias:
   ```bash
   dotnet restore
   ```

3. Compila:
   ```bash
   dotnet build
   ```

4. Ejecuta:
   ```bash
   dotnet run
   ```

5. Accede a la API en:
   ```
   http://localhost:5000
   ```

---

## Endpoints disponibles

| Método | Ruta | Descripción |
| ------ | ---- | ------------ |
| POST | /cuenta/registrar | Registra un usuario (sin validación ni hash). |
| POST | /cuenta/iniciar-sesion | Inicia sesión comparando credenciales en texto plano. |
| GET | /cuenta/perfil | Devuelve perfil; accesible sin autenticación. |

---

## Cómo usar la colección Postman

Para realizar pruebas de forma guiada:

1. Abre Postman y haz clic en **Importar**.
2. Selecciona el archivo de colección:  
   `01.-EJEMPLO_INSEGURO_CAP1_ES.postman_collection.json`
3. Postman mostrará todas las peticiones agrupadas por nombre.

**Pruebas sugeridas:**

- **Registrar Usuario**:  
  Body (JSON):  
  ```json
  {
    "nombreUsuario": "rodrigo",
    "contrasena": "1234"
  }
  ```
  Envía la solicitud y verifica que responde "Usuario registrado (INSEGURO)".

- **Registrar Usuario Vacío**:  
  Muestra que se pueden registrar usuarios con campos vacíos porque no hay validación.

- **Iniciar Sesión Correcto**:  
  Body (JSON):  
  ```json
  {
    "nombreUsuario": "rodrigo",
    "contrasena": "1234"
  }
  ```
  Respuesta esperada: mensaje de bienvenida.

- **Iniciar Sesión Contraseña Incorrecta**:  
  Body con contraseña equivocada. Respuesta: "Credenciales inválidas (INSEGURO)".

- **Iniciar Sesión Inyección SQL**:  
  Body con intento de inyección:  
  ```json
  {
    "nombreUsuario": "' OR '1'='1",
    "contrasena": "' OR '1'='1"
  }
  ```
  Aunque no funciona con EF InMemory, sirve para discutir riesgos con SQL real.

- **Consultar Perfil (Sin Autenticación)**:  
  GET directo. Demuestra que cualquier usuario (o visitante) puede ver esta ruta.

**Notas importantes:**  
- Todos los cuerpos deben enviarse en formato JSON (`Body → raw → JSON`).
- No se usa autenticación ni token.
- La base de datos se guarda en memoria, por lo que si detienes la aplicación, se pierde todo y debes registrar el usuario nuevamente.

---

## Importante

Este proyecto es únicamente para fines educativos y debe usarse solo para prácticas de auditoría y comparación con la versión segura.