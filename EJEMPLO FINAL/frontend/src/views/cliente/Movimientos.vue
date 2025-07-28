<template>
  <div class="container mt-4">
    <h2 class="mb-4 fw-semibold">Mis Movimientos</h2>

    <!-- Loading -->
    <div v-if="loading" class="text-center my-4">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>

    <!-- Error -->
    <div v-if="error" class="alert alert-danger text-center">
      {{ error }}
    </div>

    <!-- Tabla -->
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

    <!-- Sin movimientos -->
    <div v-else-if="!loading && !movimientos.length" class="text-center text-muted py-4">
      No se encontraron movimientos recientes.
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import api from '@/axios'
import { useAuthStore } from '@/store/auth'

const authStore = useAuthStore()
const movimientos = ref([])
const loading = ref(false)
const error = ref('')

onMounted(async () => {
  loading.value = true
  error.value = ''

  try {
    const response = await api.get('/transferencia/mis', {
      headers: {
        Authorization: `Bearer ${authStore.token}`
      }
    })
    movimientos.value = response.data
  } catch (err) {
    console.error('Error al obtener movimientos:', err)
    error.value = 'No se pudo cargar la información de movimientos.'
  } finally {
    loading.value = false
  }
})
</script>