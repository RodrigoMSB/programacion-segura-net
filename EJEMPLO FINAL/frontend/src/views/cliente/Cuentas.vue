<template>
  <div class="container mt-4">
    <h2 class="mb-4 fw-semibold">Mis Cuentas Bancarias</h2>

    <!-- Loading -->
    <div v-if="cuentasStore.loading" class="text-center my-4">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>

    <!-- Error -->
    <div v-if="cuentasStore.error" class="alert alert-danger text-center">
      {{ cuentasStore.error }}
    </div>

    <!-- Tabla de Cuentas -->
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
          <tr v-for="cuenta in cuentasStore.cuentas" :key="cuenta.numeroCuenta">
            <td class="text-center">{{ cuenta.numeroCuenta }}</td>
            <td class="text-center">${{ cuenta.saldo.toLocaleString() }}</td>
            <td class="text-center">
              <button class="btn btn-primary btn-sm px-3" @click="verMovimientos(cuenta.numeroCuenta)">
                Ver Movimientos
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Tabla de Movimientos -->
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
import { ref, onMounted } from 'vue'
import { useClienteCuentasStore } from '@/store/clienteCuentas'
import api from '@/axios'

const cuentasStore = useClienteCuentasStore()
const movimientos = ref([])
const cuentaSeleccionada = ref(null)

onMounted(() => {
  cuentasStore.cargarMisCuentas()
})

const verMovimientos = async (numeroCuenta) => {
  movimientos.value = []
  cuentaSeleccionada.value = numeroCuenta

  try {
    const response = await api.get(`/transferencia/por-cuenta/${numeroCuenta}`)
    movimientos.value = response.data
  } catch (error) {
    console.error('Error al obtener movimientos:', error)
  }
}
</script>