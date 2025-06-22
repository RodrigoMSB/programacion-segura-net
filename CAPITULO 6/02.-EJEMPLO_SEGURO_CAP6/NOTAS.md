# NOTAS DEL EJEMPLO SEGURO — CAPÍTULO 6

Este ejemplo implementa **prácticas criptográficas seguras**, contrastando directamente con el ejemplo inseguro.

## 🔐 Fundamentos aplicados

- **Hash de contraseñas con PBKDF2:**  
  - Utiliza salt aleatorio de 16 bytes para cada usuario.
  - Aplica 100,000 iteraciones con SHA256.
  - Almacena solo el hash y el salt, nunca la contraseña en texto claro.

- **Verificación robusta:**  
  - Recalcula el hash usando el salt almacenado.
  - Compara el resultado para validar la contraseña ingresada.

- **Cifrado AES:**  
  - Clave simétrica de 256 bits simulada para la demo.
  - Genera un **IV aleatorio único** en cada cifrado.
  - Combina `IV + Ciphertext` en un solo bloque codificado en Base64.
  - Durante descifrado, separa IV y texto cifrado para revertir la operación.

## ✅ Buenas prácticas demostradas

- Separación de responsabilidades:  
  Toda la lógica criptográfica está encapsulada en `CryptoService`.

- **Inyección de dependencias:**  
  `CryptoService` se registra como Singleton para mantener consistencia y reutilización.

- Datos sensibles y claves se mantienen fuera de hardcodeo crítico (simulación pedagógica).

- Endpoints bien definidos:
  - `POST /usuario-seguro/registrar`: Registro seguro.
  - `POST /usuario-seguro/verificar`: Verifica contraseña.
  - `GET /usuario-seguro/dato-cifrado`: Cifra dato demostrativo.
  - `POST /usuario-seguro/dato-descifrado`: Descifra dato enviado.

## ⚠️ Nota

- En producción, las claves AES deben gestionarse con **Azure Key Vault**, **AWS Secrets Manager** o hardware HSM.
- Nunca guardar claves sensibles directamente en el código fuente ni repositorios.

## 🎯 Objetivo didáctico

Este ejemplo muestra cómo aplicar correctamente:
- Principios de **Confidencialidad** y **Integridad**.
- Separación de capas: Modelo, Servicio, Controlador.
- Criptografía moderna con librerías de .NET Core.

Es un punto de partida para proyectos reales que requieran proteger credenciales y datos sensibles.

---

📚 **Fin del archivo**