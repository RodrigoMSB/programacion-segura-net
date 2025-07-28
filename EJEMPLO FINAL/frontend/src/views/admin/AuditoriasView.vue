<template>
  <div class="container mt-4">
    <h2 class="mb-3">Auditoría de Transferencias</h2>

    <!-- Mensaje de error -->
    <div v-if="error" class="alert alert-danger">
      {{ error }}
    </div>

    <!-- Tabla de auditoría -->
    <table v-if="auditorias.length" class="table table-bordered table-hover">
      <thead class="table-primary">
        <tr>
          <th>ID</th>
          <th>Cuenta Origen</th>
          <th>Cuenta Destino</th>
          <th>Monto</th>
          <th>Descripción</th>
          <th>Fecha</th>
          <th>Usuario</th>
          <th>IP</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="a in auditorias" :key="a.id">
          <td>{{ a.id }}</td>
          <td>{{ a.cuentaOrigenId }}</td>
          <td>{{ a.cuentaDestinoId }}</td>
          <td>${{ a.monto.toLocaleString() }}</td>
          <td>{{ a.descripcion || '—' }}</td>
          <td>{{ new Date(a.fecha).toLocaleString() }}</td>
          <td>{{ a.usuarioEmail }}</td>
          <td>{{ a.ip }}</td>
        </tr>
      </tbody>
    </table>

    <!-- Si no hay auditorías -->
    <div v-else class="text-muted text-center">
      No hay auditorías registradas.
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import api from '../../axios'
import { useAuthStore } from '../../store/auth'

const auditorias = ref([])
const error = ref('')
const authStore = useAuthStore()

const fetchAuditorias = async () => {
  error.value = ''
  try {
    const res = await api.get('/admin/auditorias', {
      headers: {
        Authorization: `Bearer ${authStore.token}`
      }
    })
    auditorias.value = res.data
  } catch (err) {
    console.error('Error al cargar auditorías:', err)
    error.value = 'No se pudo cargar la auditoría de transferencias.'
  }
}

onMounted(fetchAuditorias)
</script>