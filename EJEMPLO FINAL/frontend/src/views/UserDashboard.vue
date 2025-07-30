<template>
  <div>
    <!--
      NAVBAR SUPERIOR:
      Contiene el nombre del panel y muestra la sesión activa con el nombre del usuario.
      También incluye el botón para cerrar sesión.
    -->
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark px-4">
      <div class="container-fluid">
        <span class="navbar-brand">Cliente — Panel Bancario</span>

        <div class="d-flex align-items-center ms-auto">
          <span class="text-light me-3">
            <strong>Sesión:</strong>
            <!-- Si hay usuario logueado, se muestra su nombre. Si no, se indica que está cargando -->
            <span v-if="authStore.user">{{ authStore.user.nombre }}</span>
            <span v-else>Cargando...</span>
          </span>
          <!-- Botón para cerrar sesión -->
          <button @click="logout" class="btn btn-outline-light btn-sm">Cerrar sesión</button>
        </div>
      </div>
    </nav>

    <!--
      CONTENIDO PRINCIPAL:
      Contiene un menú de navegación para que el cliente acceda a sus cuentas o realice transferencias.
      El <router-view /> permite inyectar dinámicamente las sub-vistas según la ruta activa.
    -->
    <div class="container mt-4">
      <nav class="nav nav-pills mb-4">
        <RouterLink class="nav-link" to="/cliente/cuentas">Mis Cuentas</RouterLink>
        <RouterLink class="nav-link" to="/cliente/transferir">Transferir</RouterLink>
      </nav>

      <!-- Área donde se cargan las vistas hijas según la ruta activa -->
      <router-view></router-view>
    </div>
  </div>
</template>

<script setup>
// Hook para ejecutar lógica al montar el componente
import { onMounted } from 'vue'

// Componentes del sistema de rutas
import { RouterLink, RouterView, useRouter } from 'vue-router'

// Pinia store de autenticación
import { useAuthStore } from '@/store/auth'

// Se instancia el store de autenticación
const authStore = useAuthStore()

// Se accede al enrutador para redireccionar luego del logout
const router = useRouter()

// Al montar el componente, se intenta recuperar el usuario desde localStorage
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

// Método para cerrar sesión: limpia el store y redirige al login
const logout = () => {
  authStore.logout()
  router.push('/login')
}
</script>

<style scoped>
/* Si se requiere personalización de estilos, puede agregarse aquí */
</style>