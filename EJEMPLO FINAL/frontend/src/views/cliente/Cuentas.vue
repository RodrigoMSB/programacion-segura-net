<template>
  <div class="container mt-4">
    <!-- Título de la sección -->
    <h2 class="mb-4 fw-semibold">Mis Cuentas Bancarias</h2>

    <!-- Indicador visual de carga mientras se obtienen los datos -->
    <div v-if="cuentasStore.loading" class="text-center my-4">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>

    <!-- Alerta visible si hay error al cargar las cuentas -->
    <div v-if="cuentasStore.error" class="alert alert-danger text-center">
      {{ cuentasStore.error }}
    </div>

    <!-- Tabla que muestra las cuentas del cliente -->
    <div v-if="!cuentasStore.loading && cuentasStore.cuentas.length" class="card shadow-sm border-0">
      <table class="table table-hover table-bordered align-middle m-0">
        <thead class="table-primary">
          <tr>
            <th class="fw-bold text-center">Número de Cuenta</th>
            <th class="fw-bold text-center">Saldo Disponible</th>
            <th class="fw-bold text-center">Acciones</th>
          </tr>
        </thead>
        <tbody>
          <!-- Iteramos sobre la lista de cuentas obtenidas desde el store -->
          <tr v-for="cuenta in cuentasStore.cuentas" :key="cuenta.numeroCuenta">
            <td class="text-center">{{ cuenta.numeroCuenta }}</td>
            <td class="text-center">${{ cuenta.saldo.toLocaleString() }}</td>
            <td class="text-center">
              <!-- Botón para cargar movimientos de esta cuenta -->
              <button class="btn btn-primary btn-sm px-3" @click="verMovimientos(cuenta.numeroCuenta)">
                Ver Movimientos
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Tabla con los movimientos de una cuenta específica -->
    <div v-if="movimientos.length" class="card shadow-sm border-0 mt-4">
      <div class="card-header bg-primary text-white fw-semibold">
        Movimientos de Cuenta N° {{ cuentaSeleccionada }}
      </div>
      <table class="table table-bordered table-hover align-middle m-0">
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
          <!-- Mostramos cada movimiento relacionado a la cuenta seleccionada -->
          <tr v-for="mov in movimientos" :key="mov.id">
            <td class="text-center">{{ new Date(mov.fecha).toLocaleDateString() }}</td>
            <td class="text-center">{{ mov.origen }}</td>
            <td class="text-center">{{ mov.destino }}</td>
            <td
              class="text-center fw-bold"
              :class="{
                'text-success': mov.destino == cuentaSeleccionada,
                'text-danger': mov.origen == cuentaSeleccionada
              }"
            >
              ${{ mov.monto.toLocaleString() }}
            </td>
            <td class="text-center">
              {{ mov.descripcion || '—' }}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
// Importamos las funciones necesarias
import { ref, onMounted } from 'vue'
// Store con el estado y acciones relacionadas a las cuentas del cliente
import { useClienteCuentasStore } from '@/store/clienteCuentas'
// Cliente HTTP Axios configurado con token automático
import api from '@/axios'

// Instanciamos el store de cuentas
const cuentasStore = useClienteCuentasStore()
// Arreglo reactivo que contiene los movimientos de una cuenta
const movimientos = ref([])
// Número de cuenta actualmente seleccionada por el usuario
const cuentaSeleccionada = ref(null)

// Al montar el componente, disparamos la carga inicial de cuentas
onMounted(() => {
  cuentasStore.cargarMisCuentas()
})

// Función que consulta los movimientos para la cuenta seleccionada
const verMovimientos = async (numeroCuenta) => {
  movimientos.value = []
  cuentaSeleccionada.value = numeroCuenta

  try {
    // Llamada al backend para obtener movimientos por número de cuenta
    const response = await api.get(`/transferencia/por-cuenta/${numeroCuenta}`)
    movimientos.value = response.data
  } catch (error) {
    console.error('Error al obtener movimientos:', error)
  }
}
</script>