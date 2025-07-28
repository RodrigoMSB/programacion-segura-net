<template>
  <div>
    <!-- NAVBAR SUPERIOR -->
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-4">
      <div class="container-fluid">
        <span class="navbar-brand">Admin — Panel Bancario</span>

        <div class="d-flex align-items-center ms-auto">
          <span class="text-light me-3">
            <strong>Sesión:</strong>
            <span v-if="authStore.user">{{ authStore.user.nombre }}</span>
            <span v-else>Cargando...</span>
          </span>
          <button @click="logout" class="btn btn-outline-light btn-sm">Cerrar sesión</button>
        </div>
      </div>
    </nav>

    <!-- NAV INTERNO -->
    <div class="container mt-4">
      <h2 class="text-primary mb-3">Panel de Administrador</h2>

      <nav class="mb-4">
        <router-link to="/admin/usuarios" class="me-3">Usuarios</router-link>
        <router-link to="/admin/movimientos" class="me-3">Movimientos</router-link>
        <router-link to="/admin/cuentas" class="me-3">Cuentas</router-link>
        <router-link to="/admin/auditorias" class="me-3">Auditoría</router-link>
      </nav>

      <router-view />
    </div>
  </div>
</template>

<script setup>
import { useAuthStore } from '@/store/auth'
import { useRouter } from 'vue-router'

const authStore = useAuthStore()
const router = useRouter()

const logout = () => {
  authStore.logout()
  router.push('/login')
}
</script>