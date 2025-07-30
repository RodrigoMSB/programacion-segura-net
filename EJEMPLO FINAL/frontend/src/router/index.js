// -------------------------------
// IMPORTACIONES BASE
// -------------------------------

// Importamos el sistema de enrutamiento de Vue 3
import { createRouter, createWebHistory } from "vue-router";

// -------------------------------
// IMPORTACIÓN DE COMPONENTES
// -------------------------------

// Vistas públicas
import LoginView from "@/views/LoginView.vue"
import RegisterView from "@/views/RegisterView.vue"

// Dashboards por rol
import AdminDashboard from "@/views/AdminDashboard.vue"
import UserDashboard from "@/views/UserDashboard.vue"

// Vistas del panel de Admin
import AdminUsuarios from "@/views/admin/Usuarios.vue"
import AdminMovimientos from "@/views/admin/Movimientos.vue"
import AdminCuentas from "@/views/admin/AdminCuentas.vue"

// Vistas del cliente final (rol: Cliente)
import ClienteCuentas from "@/views/cliente/Cuentas.vue"
import ClienteTransferir from "@/views/cliente/Transferir.vue"

const routes = [
  // -------------------------------
  // Redirección raíz → login
  // -------------------------------
  { path: "/", redirect: "/login" },

  // -------------------------------
  // Rutas públicas
  // -------------------------------
  { path: "/login", name: "Login", component: LoginView },
  { path: "/register", name: "Register", component: RegisterView },

  // -------------------------------
  // Grupo de rutas protegidas: ADMIN
  // -------------------------------
  {
    path: "/admin",
    component: AdminDashboard, // Componente layout (contenedor general con navbar y router-view interno)
    children: [
      {
        path: "usuarios",
        name: "AdminUsuarios",
        component: AdminUsuarios
      },
      {
        path: "movimientos",
        name: "AdminMovimientos",
        component: AdminMovimientos
      },
      {
        path: "cuentas",
        name: "AdminCuentas",
        component: AdminCuentas
      },
      {
        path: "auditorias",
        name: "AdminAuditoriaCuentas",
        // Se importa de forma lazy (división de código)
        component: () => import("../views/admin/AuditoriasView.vue")
      }
    ]
  },

  // -------------------------------
  // Grupo de rutas protegidas: CLIENTE
  // -------------------------------
  {
    path: "/cliente",
    component: UserDashboard, // Componente layout del cliente
    children: [
      {
        path: "cuentas",
        name: "ClienteCuentas",
        component: ClienteCuentas
      },
      {
        path: "transferir",
        name: "ClienteTransferir",
        component: ClienteTransferir
      }
    ]
  }
]

// -------------------------------
// CREACIÓN DEL ROUTER PRINCIPAL
// -------------------------------
const router = createRouter({
  // Usamos modo de historial limpio sin hash (#)
  history: createWebHistory(),
  routes
})

export default router