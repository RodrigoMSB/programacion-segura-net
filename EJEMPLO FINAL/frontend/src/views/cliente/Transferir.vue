<template>
  <div class="container mt-4">
    <h2 class="mb-3">Transferir Fondos</h2>

    <!-- Alerta de error -->
    <div v-if="error" class="alert alert-danger">
      {{ error }}
    </div>

    <!-- Alerta de éxito -->
    <div v-if="success" class="alert alert-success">
      {{ success }}
    </div>

    <!-- Formulario -->
    <form @submit.prevent="realizarTransferencia">
      <div class="mb-3">
        <label for="origen" class="form-label">Cuenta Origen</label>
        <select v-model="form.cuentaOrigenId" class="form-select" required>
          <option disabled value="">Selecciona tu cuenta</option>
          <option v-for="c in cuentasStore.cuentas" :key="c.id" :value="c.id">
            {{ c.numeroCuenta }} - Saldo: ${{ c.saldo.toLocaleString() }}
          </option>
        </select>
      </div>

      <div class="mb-3">
        <label for="destino" class="form-label">Número Cuenta Destino</label>
        <input
          type="number"
          v-model="form.cuentaDestinoId"
          class="form-control"
          required
        />
      </div>

      <div class="mb-3">
        <label for="monto" class="form-label">Monto a Transferir</label>
        <input
          type="number"
          v-model="form.monto"
          class="form-control"
          required
          min="1"
        />
      </div>

      <button type="submit" class="btn btn-primary w-100">
        Transferir
      </button>
    </form>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import api from '@/axios'
import { useClienteCuentasStore } from '@/store/clienteCuentas'
import { useAuthStore } from '@/store/auth'

const cuentasStore = useClienteCuentasStore()
const authStore = useAuthStore()

const form = ref({
  cuentaOrigenId: '',
  cuentaDestinoId: '',
  monto: ''
})

const error = ref('')
const success = ref('')

// Cargar las cuentas del cliente al montar
onMounted(() => {
  cuentasStore.cargarMisCuentas()
})

// Enviar transferencia
const realizarTransferencia = async () => {
  error.value = ''
  success.value = ''

  try {
    await api.post(
      '/transferencia/enviar',
      {
        cuentaOrigenId: form.value.cuentaOrigenId,
        cuentaDestinoId: form.value.cuentaDestinoId,
        monto: form.value.monto
      },
      {
        headers: {
          Authorization: `Bearer ${authStore.token}`
        }
      }
    )
    success.value = '¡Transferencia realizada con éxito!'
    form.value = { cuentaOrigenId: '', cuentaDestinoId: '', monto: '' }
    await cuentasStore.cargarMisCuentas()
  } catch (err) {
    console.error('Error al transferir:', err)
    error.value = 'No se pudo completar la transferencia.'
  }
}
</script>