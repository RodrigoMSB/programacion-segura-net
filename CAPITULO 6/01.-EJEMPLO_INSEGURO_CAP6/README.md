# Ejemplo Inseguro — Capítulo 6

## Descripción

Este proyecto forma parte del **Capítulo 6: Prácticas Seguras de Programación para Criptografía**, pero implementado **de forma incorrecta a propósito**, para evidenciar malas prácticas comunes.

Su objetivo es mostrar cómo **no** se debe aplicar hashing y cifrado en una aplicación .NET, para luego comparar con la versión segura.

## Prácticas inseguras demostradas

- Hash de contraseñas usando SHA256 simple (sin *salt* ni KDF).
- Cifrado simétrico con clave y IV fijos incrustados en el código.
- Sin uso de almacenes de secretos ni rotación de claves.

## Endpoints disponibles

| Método | Ruta | Descripción |
| ------ | ---- | ------------ |
| `POST` | `/usuario/registrar` | Registra un usuario y guarda un hash SHA256 simple de su contraseña. |
| `GET` | `/usuario/dato-cifrado` | Devuelve un dato confidencial cifrado usando AES con clave y IV fijos. |
| `GET` | `/usuario/dato-descifrado` | Descifra el dato cifrado usando la misma clave y IV. |

## Cómo ejecutar el proyecto

1. Restaurar dependencias:
   ```bash
   dotnet restore
   ```

2. Compilar:
   ```bash
   dotnet build
   ```

3. Ejecutar:
   ```bash
   dotnet run
   ```

4. Acceder en:
   ```
   http://localhost:5000
   ```

## Cómo probar con Postman

### ✅ 1) Registrar usuario

**Método:** `POST`  
**URL:** `http://localhost:5000/usuario/registrar`  
**Headers:**  
- `Content-Type: application/json`

**Body (raw JSON):**
```json
{
  "nombre": "usuario1",
  "password": "MiPasswordInsegura"
}
```

**Respuesta esperada:**  
"Usuario registrado de forma insegura (hash plano)."

### ✅ 2) Obtener dato cifrado

**Método:** `GET`  
**URL:** `http://localhost:5000/usuario/dato-cifrado`  
**Headers:** Ninguno  
**Body:** Ninguno

**Respuesta esperada:**  
Cadena en Base64 representando el texto cifrado.

### ✅ 3) Obtener dato descifrado

**Método:** `GET`  
**URL:** `http://localhost:5000/usuario/dato-descifrado`  
**Headers:** Ninguno  
**Body:** Ninguno

**Respuesta esperada:**  
"Información Muy Sensible"

## Recomendación

Importa la **colección JSON de Postman** que se entrega junto al proyecto para tener todos los requests listos para ejecutar.

## Importante

✅ Este ejemplo es **inseguro a propósito** y no debe usarse en producción.  
Su único fin es demostrar errores comunes de implementación y servir de base para comparar con la versión **segura**.

## Contraste con la versión segura

Comparar con el **Ejemplo Seguro — Capítulo 6** permite entender cómo:
- Usar PBKDF2 o bcrypt con salting e iteraciones.
- Cifrar datos sensibles con IV único por mensaje.
- Gestionar claves de forma segura y rotarlas periódicamente.

## Resumen

> **Este proyecto es un anti-patrón intencional.**
> Su estudio es clave para reforzar conocimientos de criptografía aplicada y desarrollo seguro.

**Autor:** Uso exclusivo como material didáctico interno.