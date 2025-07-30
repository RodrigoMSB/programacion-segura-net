<template>
  <div class="container mt-4">
    <!-- Título principal de la vista -->
    <h2 class="mb-4 fw-semibold">Mis Movimientos</h2>

    <!-- Spinner de carga mientras se espera la respuesta del backend -->
    <div v-if="loading" class="text-center my-4">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>

    <!-- Alerta en caso de error al obtener los movimientos -->
    <div v-if="error" class="alert alert-danger text-center">
      {{ error }}
    </div>

    <!-- Tabla que se muestra si hay movimientos disponibles y no se está cargando -->
    <div v-if="!loading && movimientos.length" class="card shadow-sm border-0">
      <table class="table table-hover table-bordered align-middle m-0">
        <thead class="table-primary">
          <tr>
            <th class="fw-bold text-center">Fecha</th>
            <th class="fw-bold text-center">Cuenta Origen</th>
            <th class="fw-bold text-center">Cuenta Destino</th>
            <th class="fw-bold text-center">Monto</th>
            <th class="fw-bold text-center">Descripción</th>
          </tr>
        </thead>
        <tbody>
          <!-- Iteración sobre cada movimiento -->
          <tr v-for="m in movimientos" :key="m.id">
            <td class="text-center">{{ new Date(m.fecha).toLocaleDateString() }}</td>
            <td class="text-center">{{ m.origen }}</td>
            <td class="text-center">{{ m.destino }}</td>
            <td class="text-center">${{ m.monto.toLocaleString() }}</td>
            <td class="text-center">{{ m.descripcion }}</td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Mensaje en caso de que no existan movimientos -->
    <div v-else-if="!loading && !movimientos.length" class="text-center text-muted py-4">
      No se encontraron movimientos recientes.
    </div>
  </div>
</template>

<script setup>
// Importamos las herramientas de Vue 3
import { ref, onMounted } from 'vue'

// Cliente HTTP centralizado con interceptor de token JWT
import api from '@/axios'

// Store de autenticación para recuperar el token del usuario actual
import { useAuthStore } from '@/store/auth'

// Accedemos al store de autenticación
const authStore = useAuthStore()

// Estado reactivo que almacena los movimientos obtenidos
const movimientos = ref([])

// Estados para control de carga y errores
const loading = ref(false)
const error = ref('')

// Hook de ciclo de vida: al montar el componente se consultan los movimientos
onMounted(async () => {
  loading.value = true
  error.value = ''

  try {
    // Petición GET a endpoint seguro de movimientos personales
    const response = await api.get('/transferencia/mis', {
      headers: {
        // Autenticación con JWT del cliente actual
        Authorization: `Bearer ${authStore.token}`
      }
    })

    // Almacenamos los movimientos en el estado
    movimientos.value = response.data
  } catch (err) {
    console.error('Error al obtener movimientos:', err)
    error.value = 'No se pudo cargar la información de movimientos.'
  } finally {
    // Marcamos como finalizada la carga
    loading.value = false
  }
})
</script>