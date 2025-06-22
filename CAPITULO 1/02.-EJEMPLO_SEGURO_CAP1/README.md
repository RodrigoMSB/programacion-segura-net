
# Ejemplo Seguridad Capítulo 1 (.NET 8 + ASP.NET Core)

## Objetivo del capítulo

Introducir los conceptos fundamentales de autenticación y control de acceso básico en aplicaciones ASP.NET Core 8, usando una base de datos en memoria (`InMemoryDatabase`) para enfocarse exclusivamente en las buenas prácticas de seguridad desde el código.

Este proyecto servirá como base para comprender los siguientes conceptos clave:

- Validación de modelos (input sanitization)
- Registro de usuarios con hash de contraseña
- Inicio de sesión (autenticación manual)
- Acceso a rutas restringidas por rol
- Uso de inyección de dependencias (Dependency Injection) y separación de capas

---

## Tecnologías y herramientas utilizadas

| Tecnología       | Versión     | Descripción |
|------------------|-------------|-------------|
| .NET             | 8.0         | Framework base |
| ASP.NET Core     | 8.0         | Backend web API |
| Entity Framework | InMemory    | Persistencia temporal |
| Swagger / Swashbuckle | Última | Documentación de API |
| Postman          | Cualquiera  | Cliente para pruebas |

---

## Estructura del proyecto recomendada

```bash
EjemploSeguridadCapitulo1/
│
├── Controllers/
│   └── UsuarioController.cs
│
├── Infraestructura/
│   └── Persistencia/
│       └── ApplicationDbContext.cs
│
├── Models/
│   └── Usuario.cs
│
├── Services/
│   ├── AutenticacionService.cs
│   ├── IUsuarioService.cs
│   └── UsuarioService.cs
│
├── postman/
│   ├── SEGURIDAD_NET_Cap1_ConVariables.postman_collection.json
│   └── SEGURIDAD_NET_ENV.postman_environment.json
│
├── appsettings.json
├── launchSettings.json
├── Program.cs
├── EjemploSeguridadCapitulo1.csproj
├── README.md
├── NOTAS.md
└── ...
```

---

## Configuración de base de datos

Se utiliza `UseInMemoryDatabase` para este ejemplo:

```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("BD_Seguridad"));
```

Esto evita la necesidad de instalar un motor de base de datos y reinicia los datos en cada ejecución.

---

## Seguridad aplicada

- Contraseñas hasheadas con `SHA256` (no texto plano)
- Validación exhaustiva de campos (`[Required]`, `[EmailAddress]`, etc.)
- Uso de roles para restringir acceso
- Separación clara entre Controlador, Servicio y DbContext

---

## Endpoints disponibles

### 1. Registrar nuevo usuario

- **POST** `/api/usuario/registrar`

```json
{
  "username": "rodrigo",
  "email": "rodrigo@neitcom.cl",
  "password": "123456",
  "rol": "Admin"
}
```

### 2. Login

- **POST** `/api/usuario/login`

```json
{
  "username": "rodrigo",
  "password": "123456"
}
```

### 3. Listar usuarios

- **GET** `/api/usuario`

```json
[
  {
    "id": 1,
    "username": "rodrigo",
    "email": "rodrigo@neitcom.cl",
    "rol": "Admin"
  }
]
```

---

## Pruebas con Postman

Para probar todos los endpoints del proyecto con Postman, es necesario importar dos archivos:

### 1. `SEGURIDAD_NET_ENV.postman_environment.json`

Este archivo define el entorno de ejecución con variables como:

- `host`: `localhost`
- `port`: `5001`
- `baseUrl`: `https://localhost:5001`

Cómo importar y activar:

1. Abre Postman
2. Ve a la sección "Environments"
3. Haz clic en `Import` y selecciona el archivo
4. Actívalo en la parte superior derecha de Postman

### 2. `SEGURIDAD_NET_Cap1_ConVariables.postman_collection.json`

Esta colección incluye las peticiones listas para usar:

- `POST /registrar`
- `POST /login`
- `GET /listar`

Todos los endpoints usan la variable `{{baseUrl}}`, evitando modificar la URL manualmente.

Cómo importar:

1. Ve a la sección "Collections"
2. Haz clic en `Import` y selecciona el archivo
3. Ejecuta los endpoints directamente

Con estos dos archivos, puedes probar la API completa sin escribir una sola URL.

---

## Ejecución

```bash
dotnet run
```

Swagger disponible en: [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html)

---

## Archivos generados automáticamente

Puedes ignorar o excluir en Git estos directorios:

- `bin/`
- `obj/`

Agrega esto a tu `.gitignore`:

```
bin/
obj/
*.db
*.suo
*.user
```

---

## Conclusión

Este proyecto demuestra de forma clara y práctica cómo implementar los fundamentos de seguridad en aplicaciones ASP.NET Core 8.
Sirve como base para entender la validación de entradas, la gestión segura de contraseñas mediante hashing, el control de acceso basado en roles y la separación de responsabilidades siguiendo una arquitectura limpia.
Además, integra herramientas como Swagger y Postman para facilitar la documentación y las pruebas de la API.

Esta estructura y estos principios sentarán las bases para los capítulos siguientes, donde se incorporarán mecanismos más avanzados de autenticación, autorización y protección frente a vulnerabilidades reales.

Se recomienda utilizar este ejemplo como punto de partida para experimentar, modificar y reforzar las buenas prácticas de desarrollo seguro en entornos reales.
