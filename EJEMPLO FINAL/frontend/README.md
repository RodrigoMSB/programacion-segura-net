# 💻 Frontend — Banco Seguro App

Este proyecto corresponde a la **interfaz cliente** del sistema "Banco Seguro", una aplicación bancaria moderna desarrollada con Vue 3 + Composition API + Bootstrap 5. Permite realizar operaciones como registro, login, gestión de usuarios, visualización de cuentas y movimientos, y transferencias seguras entre cuentas.

---

## ✅ Tecnologías utilizadas

- [Vue 3](https://vuejs.org/) (Composition API + `<script setup>`)
- [Pinia](https://pinia.vuejs.org/) para manejo de estado
- [Vue Router](https://router.vuejs.org/)
- [Axios](https://axios-http.com/) para consumo de API REST
- [Bootstrap 5](https://getbootstrap.com/) para estilos y layout
- [jwt-decode](https://github.com/auth0/jwt-decode) para decodificar JWT

---

## 📦 Requisitos

Antes de iniciar, asegúrate de tener instalado lo siguiente:

- Node.js (v18 o superior) 👉 https://nodejs.org
- npm (v9 o superior) o yarn

---

## 🚀 Instalación y ejecución local

1. **Clonar el repositorio**

```bash
git clone https://github.com/tu-usuario/banco-seguro-frontend.git
cd banco-seguro-frontend
```

2. **Instalar dependencias**

```bash
npm install
# o
yarn install
```

npm install jwt-decode

3. **Ejecutar la aplicación**

```bash
npm run dev
# o
yarn dev
```

4. **Abrir en el navegador**

Dirígete a:  
👉 `http://localhost:5173`

---

## 🏗️ Estructura del proyecto

```bash
src/
│
├── components/         # (opcional) Componentes reutilizables
├── router/             # Rutas de la aplicación
│   └── index.js
├── store/              # Pinia Stores (auth, cuentas, usuarios)
│   ├── auth.js
│   ├── cuentas.js
│   ├── clienteCuentas.js
│   └── usuarios.js
├── views/              # Vistas principales y por rol
│   ├── LoginView.vue
│   ├── RegisterView.vue
│   ├── AdminDashboard.vue
│   ├── UserDashboard.vue
│   └── admin/
│       ├── AuditoriasView.vue
│       ├── AdminCuentas.vue
│       ├── Movimientos.vue
│       ├── Usuarios.vue
│   └── cliente/
│       ├── Cuentas.vue
│       ├── Movimientos.vue
│       └── Transferir.vue
├── App.vue             # Layout principal
├── main.js             # Punto de entrada principal
└── axios.js            # Configuración central de Axios
```

---

## 🔐 Seguridad y autenticación

- El sistema utiliza **JWT** para autenticación.
- El token se guarda en `localStorage`.
- El `axios` incluye automáticamente el header `Authorization` con cada petición protegida.
- El frontend decodifica el JWT para mostrar la información del usuario logueado.

---

## 🧪 Estado y lógica reactiva

- El sistema utiliza **Pinia** como manejador de estado.
- La autenticación, las cuentas y los usuarios están desacoplados en `stores` específicos para facilitar el mantenimiento y testing.
- Toda la lógica está escrita en **Composition API** para un código más limpio, moderno y escalable.

---

## 🔧 Configuración adicional

Si el backend cambia de puerto o dominio, edita el archivo `src/axios.js`:

```js
const api = axios.create({
  baseURL: 'https://localhost:5001', // Cambiar si es necesario
})
```

---

## 🎓 Recomendaciones para principiantes en Vue

Si estás comenzando con Vue, se recomienda revisar los siguientes recursos:

- [Documentación oficial Vue 3](https://vuejs.org/guide/introduction.html)
- [Guía rápida de Pinia](https://pinia.vuejs.org/introduction.html)
- [Curso básico de Vue Router](https://router.vuejs.org/guide/)
- [Axios para principiantes](https://axios-http.com/docs/intro)

---

## ✍️ Autoría

Este proyecto fue desarrollado como parte del curso **Programación Segura .NET**.
