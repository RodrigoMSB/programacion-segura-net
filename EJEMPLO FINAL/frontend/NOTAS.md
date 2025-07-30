# **Notas del Frontend — Proyecto Banco Seguro .NET**

Este documento explica **a detalle** la arquitectura y el funcionamiento del **frontend** de nuestro ejemplo final.  
El proyecto fue desarrollado usando **Vue 3 + Vite + Pinia + Vue Router + Axios + Bootstrap 5**, consumiendo el backend en **.NET 8**.

---

## **1. Tecnologías y Librerías**

- **Vue 3 (Composition API)**  
  Framework principal para el desarrollo de SPA (Single Page Application).

- **Vite**  
  Entorno de construcción y servidor de desarrollo rápido para Vue 3.

- **Pinia**  
  Librería de gestión de estado centralizada (reemplazo moderno de Vuex).

- **Vue Router**  
  Sistema de ruteo para manejar vistas dinámicas (Login, Dashboard Cliente, Dashboard Admin).

- **Axios**  
  Cliente HTTP para consumir la API del backend .NET.

- **Bootstrap 5**  
  Framework CSS para diseño responsivo y componentes estilizados.

- **jwt-decode**  
  Librería para decodificar JWT y extraer información del usuario logeado.

---

## **2. Estructura de Carpetas del Frontend**

```
frontend/
│
├─ public/                 # Archivos públicos, favicon, etc.
├─ src/
│  ├─ assets/              # Imágenes, íconos, estilos personalizados
│  ├─ components/          # Componentes reutilizables (botones, layouts)
│  ├─ views/               # Vistas principales (Login, Registro, Dashboards)
│  │  ├─ admin/            # Vistas específicas de administración
│  │  └─ cliente/          # Vistas específicas de clientes
│  ├─ store/               # Pinia stores (auth, usuarios, cuentas)
│  ├─ router/              # Configuración de rutas de la SPA
│  ├─ axios.js             # Configuración central de Axios
│  ├─ App.vue              # Componente raíz
│  └─ main.js              # Punto de entrada de la aplicación
│
├─ index.html              # HTML base que monta el frontend
└─ package.json            # Dependencias y scripts del proyecto
```

---

## **3. Flujo Principal del Frontend**

### **a Autenticación**

1. **LoginView.vue**:
   - El usuario ingresa email y contraseña.
   - Se envía POST a `https://localhost:5001/auth/login`.
   - Si es correcto, se guarda **JWT** y **rol** en `localStorage` y en `Pinia`.

2. **RegisterView.vue**:
   - Permite completar el registro del cliente pendiente.
   - Envía POST a `/auth/completar-registro`.
   - Tras registrar, redirige al login automáticamente.

3. **auth.js (Pinia)**:
   - Maneja el estado `token`, `role` y `user`.
   - Permite persistir la sesión en `localStorage`.
   - Incluye método `logout()` para limpiar datos.

---

### **b Rutas y Protección de Vistas**

- **`router.js`** define:
  - `/login` → Login
  - `/register` → Registro
  - `/admin/...` → Panel Admin con subrutas
  - `/cliente/...` → Panel Cliente con subrutas

Cada panel carga su **Dashboard** (AdminDashboard.vue / UserDashboard.vue)  
y dentro, un `<router-view />` muestra la sección seleccionada.

---

### **c Gestión de Estado (Pinia)**

- **`store/auth.js`** → Maneja sesión y JWT.  
- **`store/usuarios.js`** → Admin: listar y crear clientes.  
- **`store/cuentas.js`** → Admin: listar y crear cuentas.  
- **`store/clienteCuentas.js`** → Cliente: ver sus cuentas.

Ventajas:
- Centraliza llamadas a la API.
- Permite reactividad en componentes al actualizar datos.
- Evita duplicación de lógica.

---

### **d Consumo del Backend con Axios**

- Archivo **`src/axios.js`**:
  - Configura `baseURL` (`https://localhost:5001`).
  - Añade automáticamente `Authorization: Bearer <token>` si existe.
  
- Ejemplo de uso:
```js
const response = await api.get('/cuenta/mis-cuentas')
```
