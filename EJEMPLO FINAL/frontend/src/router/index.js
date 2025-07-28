import { createRouter, createWebHistory } from "vue-router";
import LoginView from "@/views/LoginView.vue";
import RegisterView from "@/views/RegisterView.vue";
import AdminDashboard from "@/views/AdminDashboard.vue";
import UserDashboard from "@/views/UserDashboard.vue";

import AdminUsuarios from "@/views/admin/Usuarios.vue";
import AdminMovimientos from "@/views/admin/Movimientos.vue";
import AdminCuentas from "@/views/admin/AdminCuentas.vue";

import ClienteCuentas from "@/views/cliente/Cuentas.vue";
import ClienteMovimientos from "@/views/cliente/Movimientos.vue";
import ClienteTransferir from "@/views/cliente/Transferir.vue";

const routes = [
  { path: "/", redirect: "/login" },
  { path: "/login", name: "Login", component: LoginView },
  { path: "/register", name: "Register", component: RegisterView },

  // Admin
  {
    path: "/admin",
    component: AdminDashboard,
    children: [
      { path: "usuarios", name: "AdminUsuarios", component: AdminUsuarios },
      {
        path: "movimientos",
        name: "AdminMovimientos",
        component: AdminMovimientos,
      },
      { path: "cuentas", name: "AdminCuentas", component: AdminCuentas },
      {
        path: "auditorias",name: "AdminAuditoriaCuentas",
        component: () => import("../views/admin/AuditoriasView.vue"),
      },
    ],
  },

  // Cliente
  {
    path: "/cliente",
    component: UserDashboard,
    children: [
      { path: "cuentas", name: "ClienteCuentas", component: ClienteCuentas },
      {
        path: "transferir",
        name: "ClienteTransferir",
        component: ClienteTransferir,
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
