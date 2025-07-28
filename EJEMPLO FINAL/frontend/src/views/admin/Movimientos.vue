<template>
  <div class="container mt-4">
    <h2 class="mb-3">Listado de Movimientos</h2>

    <!-- Mensaje de error -->
    <div v-if="error" class="alert alert-danger">
      {{ error }}
    </div>

    <!-- Tabla de movimientos -->
    <table v-if="movimientos.length" class="table table-bordered table-hover">
      <thead class="table-primary">
        <tr>
          <th>ID</th>
          <th>Cuenta Origen</th>
          <th>Cuenta Destino</th>
          <th>Monto</th>
          <th>Fecha</th>
          <th>Descripción</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="mov in movimientos" :key="mov.id">
          <td>{{ mov.id }}</td>
          <td>{{ mov.origen || '-' }}</td>
          <td>{{ mov.destino || '-' }}</td>
          <td>${{ mov.monto.toLocaleString() }}</td>
          <td>{{ new Date(mov.fecha).toLocaleString() }}</td>
          <td>{{ mov.descripcion || '-' }}</td>
        </tr>
      </tbody>
    </table>

    <!-- Si no hay movimientos -->
    <div v-else class="text-muted text-center">
      No hay movimientos para mostrar.
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import api from '@/axios'
import { useAuthStore } from '@/store/auth'

const movimientos = ref([])
const error = ref('')
const authStore = useAuthStore()

// Cargar movimientos del backend admin
const fetchMovimientos = async () => {
  error.value = ''
  try {
    const res = await api.get('/admin/movimientos', {
      headers: {
        Authorization: `Bearer ${authStore.token}`
      }
    })
    movimientos.value = res.data
  } catch (err) {
    console.error('Error al cargar movimientos:', err)
    error.value = 'No se pudo cargar la lista de movimientos.'
  }
}

onMounted(fetchMovimientos)
</script>