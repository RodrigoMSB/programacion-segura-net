<template>
  <div class="container mt-4">
    <h2 class="mb-3">Listado de Movimientos</h2>

    <!-- Muestra un mensaje de error si ocurre una falla al cargar -->
    <div v-if="error" class="alert alert-danger">
      {{ error }}
    </div>

    <!-- Tabla que muestra los movimientos si existen -->
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
        <!-- Recorremos cada movimiento y lo mostramos -->
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

    <!-- Mensaje en caso de que no existan movimientos -->
    <div v-else class="text-muted text-center">
      No hay movimientos para mostrar.
    </div>
  </div>
</template>

<script setup>
// Importamos funciones de composición de Vue
import { ref, onMounted } from 'vue'

// Cliente HTTP con configuración base y token automático
import api from '@/axios'

// Store de autenticación para obtener el token del usuario actual
import { useAuthStore } from '@/store/auth'

// Referencia reactiva que almacenará la lista de movimientos
const movimientos = ref([])

// Referencia para manejar posibles errores en la carga
const error = ref('')

// Obtenemos el store de autenticación
const authStore = useAuthStore()

// Función para cargar los movimientos desde el backend del administrador
const fetchMovimientos = async () => {
  error.value = '' // Reseteamos cualquier error previo

  try {
    // Petición GET autenticada al endpoint de movimientos (admin)
    const res = await api.get('/admin/movimientos', {
      headers: {
        Authorization: `Bearer ${authStore.token}` // Se incluye el token JWT
      }
    })

    // Guardamos los datos recibidos
    movimientos.value = res.data
  } catch (err) {
    console.error('Error al cargar movimientos:', err)
    error.value = 'No se pudo cargar la lista de movimientos.'
  }
}

// Ejecutamos al montar el componente
onMounted(fetchMovimientos)
</script>