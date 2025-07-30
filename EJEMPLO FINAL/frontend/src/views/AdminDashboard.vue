<template>
  <div>
    <!-- NAVBAR SUPERIOR -->
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-4">
      <div class="container-fluid">
        <!-- Título del panel -->
        <span class="navbar-brand">Admin — Panel Bancario</span>

        <!-- Sección lateral con info de sesión y botón de logout -->
        <div class="d-flex align-items-center ms-auto">
          <span class="text-light me-3">
            <strong>Sesión:</strong>
            <!-- Mostramos nombre del usuario si existe -->
            <span v-if="authStore.user">{{ authStore.user.nombre }}</span>
            <span v-else>Cargando...</span>
          </span>

          <!-- Botón para cerrar sesión -->
          <button @click="logout" class="btn btn-outline-light btn-sm">Cerrar sesión</button>
        </div>
      </div>
    </nav>

    <!-- CONTENIDO PRINCIPAL -->
    <div class="container mt-4">
      <!-- Título de sección -->
      <h2 class="text-primary mb-3">Panel de Administrador</h2>

      <!-- Navegación interna entre vistas del admin -->
      <nav class="mb-4">
        <!-- Cada link dirige a una ruta diferente anidada bajo /admin -->
        <router-link to="/admin/usuarios" class="me-3">Usuarios</router-link>
        <router-link to="/admin/movimientos" class="me-3">Movimientos</router-link>
        <router-link to="/admin/cuentas" class="me-3">Cuentas</router-link>
        <router-link to="/admin/auditorias" class="me-3">Auditoría</router-link>
      </nav>

      <!-- Aquí se renderiza la vista correspondiente según la ruta activa -->
      <router-view />
    </div>
  </div>
</template>

<script setup>
// Importamos el store de autenticación para acceder al usuario actual
import { useAuthStore } from '@/store/auth'
// Importamos el router para poder redirigir al cerrar sesión
import { useRouter } from 'vue-router'

// Instanciamos store y router
const authStore = useAuthStore()
const router = useRouter()

// Función para cerrar sesión y redirigir al login
const logout = () => {
  authStore.logout()       // Limpiamos el token y datos del usuario
  router.push('/login')    // Redirigimos al login
}
</script>