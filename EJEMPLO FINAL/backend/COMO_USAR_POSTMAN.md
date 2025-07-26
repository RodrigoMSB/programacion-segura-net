# 🧪 Cómo Usar los Archivos de Postman para el Proyecto SEGURIDAD_BANCO_FINAL

Este archivo explica paso a paso cómo utilizar la colección y el entorno de Postman que se han generado para probar el backend del proyecto **SEGURIDAD_BANCO_FINAL**. No se necesita realizar ninguna configuración adicional: todo está preconfigurado para que puedas probar el sistema de inmediato.

---

## 📁 Archivos Necesarios

1. `SEGURIDAD_BANCO_FINAL_Collection.postman_collection.json`
2. `SEGURIDAD_BANCO_FINAL_Env.postman_environment.json`

> Ambos archivos deben ser importados en Postman.

---

## 🧭 Paso 1: Importar los Archivos en Postman

1. Abre Postman.
2. En la barra lateral izquierda:
   - Haz clic en **"Collections"** → botón **"Import"** (ícono con flecha hacia abajo).
   - Selecciona el archivo `SEGURIDAD_BANCO_FINAL_Collection.postman_collection.json`.

3. Luego haz clic en **"Environments"** (ícono del globo terráqueo):
   - Clic en **"Import"**.
   - Selecciona el archivo `SEGURIDAD_BANCO_FINAL_Env.postman_environment.json`.

---

## ⚙️ Paso 2: Seleccionar el Entorno

1. Arriba a la derecha de Postman, selecciona el entorno llamado:
   - `SEGURIDAD_BANCO_FINAL_ENV`

Esto habilita automáticamente las variables `base_url`, `token_cliente`, `token_admin`, entre otras.

---

## 🔐 Paso 3: Autenticarse (Login)

1. Dentro de la colección, expande la carpeta **Auth**.
2. Ejecuta:
   - `Login Admin` o `Login Cliente` según el tipo de usuario.

> Si las credenciales son correctas, el token se guardará automáticamente en la variable correspondiente (`token_admin` o `token_cliente`).

---

## 📤 Paso 4: Probar Endpoints Protegidos

1. Navega por las carpetas **Cliente - Flujos** o **Admin - Flujos**.
2. Ejecuta las solicitudes:
   - `Enviar Transferencia`
   - `Consultar Mis Cuentas`
   - `Listar Usuarios`
   - `Ver Todos los Movimientos`
   - etc.

> Los tokens serán enviados automáticamente en el encabezado `Authorization` como `Bearer {token}`.

---

## 🔁 Regenerar la Base de Datos (Opcional)

Si necesitas reiniciar el sistema desde cero:

```bash
dotnet dev-certs https --trust
dotnet clean
dotnet build
dotnet run
dotnet ef database update
```

Si necesitas eliminar las tablas manualmente (modo avanzado):

```sql
DROP TABLE IF EXISTS Movimientos;
DROP TABLE IF EXISTS CuentasBancarias;
DROP TABLE IF EXISTS Usuarios;
```

---

## 📝 Notas Finales

- Swagger **no está habilitado**, el sistema se testea exclusivamente por Postman.
- Todos los endpoints funcionan usando `https://localhost:5001` como base.
- Si cambias el puerto, recuerda actualizar la variable `base_url` en el entorno de Postman.


