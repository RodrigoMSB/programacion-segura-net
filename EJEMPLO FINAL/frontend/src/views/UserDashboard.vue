<template>
  <div>
    <!-- NAVBAR SUPERIOR -->
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-4">
      <div class="container-fluid">
        <span class="navbar-brand">Cliente — Panel Bancario</span>

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

    <!-- CONTENIDO PRINCIPAL -->
    <div class="container mt-4">
      <nav class="nav nav-pills mb-4">
        <RouterLink class="nav-link" to="/cliente/cuentas">Mis Cuentas</RouterLink>
        <RouterLink class="nav-link" to="/cliente/transferir">Transferir</RouterLink>
      </nav>

      <router-view></router-view>
    </div>
  </div>
</template>

<script setup>
import { onMounted } from 'vue'
import { RouterLink, RouterView, useRouter } from 'vue-router'
import { useAuthStore } from '@/store/auth'

const authStore = useAuthStore()
const router = useRouter()

onMounted(() => {
  const raw = localStorage.getItem('user')
  if (raw && raw !== 'undefined') {
    try {
      authStore.user = JSON.parse(raw)
    } catch (error) {
      console.error('Error al parsear usuario del localStorage:', error)
      authStore.user = null
    }
  }
})

const logout = () => {
  authStore.logout()
  router.push('/login')
}
</script>