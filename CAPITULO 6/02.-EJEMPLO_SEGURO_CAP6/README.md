# ✅ Ejemplo SEGURO — Capítulo 6: Prácticas Seguras de Criptografía

Este proyecto implementa **prácticas seguras de almacenamiento de contraseñas y cifrado de datos sensibles** en .NET 8, corrigiendo todos los problemas mostrados en la versión insegura.

---

## 📌 **Características clave**

- 🔑 **Hash de contraseñas con PBKDF2:**  
  - Salt aleatorio único por usuario.
  - 100,000 iteraciones usando SHA256.
  - No se almacena la contraseña original, solo `Hash` y `Salt`.

- 🔐 **Cifrado seguro con AES:**  
  - Clave de 256 bits simulada para la demo.
  - IV aleatorio generado por cada cifrado.
  - El IV se embebe junto al texto cifrado (formato `IV + Ciphertext` en Base64).

- ✅ **Separación de responsabilidades:**  
  - `CryptoService` gestiona toda la lógica criptográfica.
  - `UsuarioSeguroController` orquesta registro, verificación y cifrado/descifrado.

---

## ⚙️ **Ejecutar el proyecto**

1️⃣ Restaurar dependencias:
```bash
dotnet restore
```

2️⃣ Compilar:
```bash
dotnet build
```

3️⃣ Ejecutar:
```bash
dotnet run
```

4️⃣ La API escuchará en:
```
http://localhost:5000
```

---

## 🧪 **Probar la API con Postman**

### 1️⃣ Registrar usuario seguro

- **Método:** `POST`
- **URL:** `http://localhost:5000/usuario-seguro/registrar`
- **Body (JSON):**
  ```json
  {
    "nombre": "usuario1",
    "hashPassword": "MiPasswordFuerte123!"
  }
  ```

### 2️⃣ Verificar contraseña

- **Método:** `POST`
- **URL:** `http://localhost:5000/usuario-seguro/verificar`
- **Body (JSON):**
  ```json
  {
    "nombre": "usuario1",
    "hashPassword": "MiPasswordFuerte123!"
  }
  ```

### 3️⃣ Obtener dato cifrado

- **Método:** `GET`
- **URL:** `http://localhost:5000/usuario-seguro/dato-cifrado`

### 4️⃣ Descifrar dato

- **Método:** `POST`
- **URL:** `http://localhost:5000/usuario-seguro/dato-descifrado`
- **Body (raw text, no JSON):**
  ```
  <pega aquí el ciphertext Base64 obtenido del paso anterior>
  ```

---

## ⚠️ **Notas de seguridad**

- La clave AES está embebida solo como demostración; **en proyectos reales se debe usar un gestor de claves seguro** (Azure Key Vault, AWS KMS, etc.).
- Se muestra cómo generar IV dinámico, que es esencial para evitar vulnerabilidades por reutilización de nonce.

---

## 🎓 **Propósito didáctico**

Este ejemplo es parte del **Capítulo 6 — Prácticas Seguras de Criptografía** del curso de desarrollo seguro en .NET Core. Sirve para:

- Contrastar prácticas inseguras vs. seguras.
- Enseñar PBKDF2, salt, iteraciones y hash robusto.
- Demostrar cifrado AES correctamente aplicado con IV aleatorio.

---

✅ **Fin del archivo — listo para producción didáctica**