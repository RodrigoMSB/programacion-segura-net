# NOTAS DEL EJEMPLO INSEGURO — CAPÍTULO 6

## Objetivo del ejemplo

Este proyecto tiene como propósito **demostrar de forma explícita qué prácticas de criptografía NO se deben aplicar** en aplicaciones reales. Sirve como referencia para contrastar con el ejemplo seguro y para reforzar la comprensión de los principios de protección de contraseñas y cifrado de datos sensibles.

---

## Prácticas inseguras ilustradas

- **Almacenamiento de contraseñas:**
  - Se aplica un hash SHA256 directo, sin uso de *salt* ni algoritmos de derivación de clave (KDF) como PBKDF2 o bcrypt.
  - Esta técnica es vulnerable a ataques de diccionario y fuerza bruta.

- **Cifrado de información confidencial:**
  - Se utiliza el algoritmo AES con una clave simétrica **hardcodeada** directamente en el código fuente.
  - El Vector de Inicialización (IV) es fijo y está expuesto.
  - Reutilizar IV con la misma clave anula la seguridad del cifrado simétrico.

- **Gestión de secretos:**
  - No existe ningún mecanismo para proteger la clave ni el IV.
  - No se emplean almacenes de secretos como Azure Key Vault, AWS KMS o entornos seguros.
  - No hay rotación ni actualización de claves.

---

## Riesgos asociados

Estas malas prácticas permiten que un atacante con acceso al código fuente pueda:
- Recuperar la clave simétrica y descifrar datos protegidos.
- Realizar ataques de fuerza bruta sobre contraseñas con alta probabilidad de éxito.
- Reutilizar IVs facilita ataques de criptoanálisis y expone patrones en datos cifrados.

---

## Valor didáctico

Este ejemplo sirve como base para:
- Analizar vulnerabilidades reales que aún se encuentran en aplicaciones de producción mal implementadas.
- Practicar auditorías de código inseguro.
- Contrastar con la implementación correcta que se desarrolla en la **versión segura del Capítulo 6**.

---

## Resumen

**Este código NO debe usarse nunca en producción.**  
Su único propósito es fortalecer la comprensión de conceptos fundamentales de criptografía aplicada, enfatizando la importancia de seguir prácticas modernas y estándares robustos para proteger información crítica.

---
