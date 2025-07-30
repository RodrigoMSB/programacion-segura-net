<template>
  <div class="container mt-4">
    <h2 class="mb-3">Auditoría de Transferencias</h2>

    <!-- Sección para mostrar un mensaje de error si ocurre una falla al cargar -->
    <div v-if="error" class="alert alert-danger">
      {{ error }}
    </div>

    <!-- Tabla que muestra los registros de auditoría si existen -->
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

    <!-- Mensaje en caso de que no existan registros -->
    <div v-else class="text-muted text-center">
      No hay auditorías registradas.
    </div>
  </div>
</template>

<script setup>
// Composición API de Vue
import { ref, onMounted } from 'vue'

// Cliente HTTP con token automático
import api from '../../axios'

// Store de autenticación para obtener el token JWT
import { useAuthStore } from '../../store/auth'

// Lista de auditorías recibidas desde el backend
const auditorias = ref([])

// Variable para guardar errores si ocurren
const error = ref('')

// Store que maneja los datos del usuario actual (token incluido)
const authStore = useAuthStore()

// Función para obtener la lista de auditorías desde el backend
const fetchAuditorias = async () => {
  error.value = '' // limpiar errores previos
  try {
    // Llamado GET autenticado al endpoint del backend
    const res = await api.get('/admin/auditorias', {
      headers: {
        Authorization: `Bearer ${authStore.token}`
      }
    })

    // Guardamos la respuesta (array de objetos de auditoría)
    auditorias.value = res.data
  } catch (err) {
    console.error('Error al cargar auditorías:', err)
    error.value = 'No se pudo cargar la auditoría de transferencias.'
  }
}

// Ejecutar cuando el componente se monte
onMounted(fetchAuditorias)
</script>