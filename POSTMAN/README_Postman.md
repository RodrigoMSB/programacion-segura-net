
# 🧪 Colecciones Postman — Curso Seguridad en Aplicaciones .NET

Este archivo `README_Postman.md` documenta el uso de las colecciones Postman para todos los ejemplos del curso de Seguridad en Aplicaciones .NET. Contiene instrucciones claras para importar y ejecutar las pruebas en cada uno de los capítulos, tanto en sus versiones inseguras como seguras.

---

## 🗂️ Estructura de Archivos

Los archivos están nombrados con el siguiente patrón:

```
<NUMERO_CAPITULO>.-EJEMPLO_<TIPO>_CAP<NUMERO>.json
```

Por ejemplo:

- `01.-EJEMPLO_INSEGURO_CAP1.json`
- `01.-EJEMPLO_SEGURO_CAP1.json`
- `FINAL_EJEMPLO_SEGURIDAD_BANCO.json`

También existen archivos de entorno como:

- `01.-EJEMPLO_SEGURO_CAP1_ENV.json`
- `FINAL_ENV_EJEMPLO_SEGURIDAD_BANCO.json`

---

## 🚀 Cómo importar las colecciones y entornos en Postman

### 1. Abrir Postman

Asegúrate de tener instalada la aplicación [Postman](https://www.postman.com/downloads/).

---

### 2. Importar las colecciones

1. Haz clic en el botón **"Import"** (arriba a la izquierda de la interfaz de Postman).
2. Selecciona la pestaña **"Upload Files"**.
3. Arrastra los archivos `.json` correspondientes a las colecciones que deseas cargar. Por ejemplo:

```
01.-EJEMPLO_INSEGURO_CAP1.json
01.-EJEMPLO_SEGURO_CAP1.json
...
FINAL_EJEMPLO_SEGURIDAD_BANCO.json
```

4. Postman los clasificará automáticamente como colecciones.

---

### 3. Importar los archivos de entorno

1. Ve al engranaje ⚙️ en la parte superior derecha de Postman y haz clic en **"Environments"**.
2. Presiona **"Import"** y selecciona los archivos `.json` del entorno, por ejemplo:

```
01.-EJEMPLO_SEGURO_CAP1_ENV.json
FINAL_ENV_EJEMPLO_SEGURIDAD_BANCO.json
```

3. Activa el entorno seleccionado desde la barra superior desplegable de entornos.

---

### 4. Usar las variables de entorno

Algunas colecciones utilizan variables como `{{base_url}}`, `{{token_admin}}` o `{{token_cliente}}`. Estas deben estar correctamente definidas en el entorno activo.

Por ejemplo, en `FINAL_ENV_EJEMPLO_SEGURIDAD_BANCO.json` están predefinidas:

```json
{
  "base_url": "https://localhost:5001",
  "token_admin": "",
  "token_cliente": ""
}
```

Una vez que hagas login (Admin o Cliente), puedes copiar el token del response y pegarlo en la variable correspondiente en el entorno activo.

---

## 📚 Recomendaciones

- Verifica que el backend correspondiente esté ejecutándose antes de hacer las pruebas.
- Si tienes errores de conexión, revisa el `base_url` y el puerto.
- En entornos Windows recuerda permitir tráfico HTTPS en `localhost`.

---

## 📦 ¿Puedo importar todo junto?

¡Sí! Puedes seleccionar todos los archivos `.json` y arrastrarlos a Postman. Las colecciones y entornos se importarán automáticamente en sus respectivas secciones.

---

## ✅ Buenas prácticas

- Usa siempre entornos con variables para mantener tus colecciones limpias y reutilizables.
- Recuerda seleccionar el entorno activo antes de ejecutar una prueba que contenga `{{variable}}`.

---

## ✉️ Soporte

Si alguna colección falla o deseas reportar un error, contacta al profesor del curso o abre un issue en el repositorio del proyecto.


