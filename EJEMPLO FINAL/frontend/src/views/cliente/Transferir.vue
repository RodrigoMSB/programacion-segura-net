<template>
  <div class="container mt-4">
    <h2 class="mb-3">Transferir Fondos</h2>

    <!-- Alertas -->
    <div v-if="error" class="alert alert-danger">{{ error }}</div>
    <div v-if="success" class="alert alert-success">{{ success }}</div>

    <!-- Formulario -->
    <form @submit.prevent="realizarTransferencia" novalidate>
      <!-- Cuenta Origen -->
      <div class="mb-3">
        <label for="origen" class="form-label">Cuenta Origen (ID)</label>
        <select v-model="form.cuentaOrigenId" class="form-select" required>
          <option disabled value="">Selecciona tu cuenta</option>
          <option v-for="c in cuentasStore.cuentas" :key="c.id" :value="c.id">
            {{ c.numeroCuenta }} - Saldo: ${{ c.saldo.toLocaleString() }}
          </option>
        </select>
        <small class="text-muted">
          Saldo disponible: {{
            cuentasStore.cuentas.find(c => c.id === form.cuentaOrigenId)?.saldo.toLocaleString() || 'N/A'
          }}
        </small>
      </div>

      <!-- Cuenta Destino -->
      <div class="mb-3">
        <label for="destino" class="form-label">ID Cuenta Destino</label>
        <input
          type="number"
          v-model="form.cuentaDestinoId"
          class="form-control"
          required
          min="1"
          @input="form.cuentaDestinoId = form.cuentaDestinoId.toString().slice(0, 9)"
        />
      </div>

      <!-- Monto -->
      <div class="mb-3">
        <label for="monto" class="form-label">Monto a Transferir</label>
        <input
          type="number"
          v-model.number="form.monto"
          class="form-control"
          required
          min="1"
          max="100000000"
        />
      </div>

      <!-- Descripción -->
      <div class="mb-3">
        <label for="descripcion" class="form-label">Descripción (opcional)</label>
        <input
          type="text"
          v-model.trim="form.descripcion"
          class="form-control"
          maxlength="100"
          placeholder="Ej: Pago arriendo, envío a mamá..."
        />
        <small class="form-text text-muted">Máximo 100 caracteres</small>
      </div>

      <!-- Botón -->
      <button type="submit" class="btn btn-primary w-100">Transferir</button>
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
  monto: '',
  descripcion: '' // NUEVO
})

const error = ref('')
const success = ref('')

// Cargar cuentas al montar
onMounted(() => {
  cuentasStore.cargarMisCuentas()
})

// Validación y envío
const realizarTransferencia = async () => {
  error.value = ''
  success.value = ''

  // Validaciones básicas en frontend
  if (!form.value.cuentaOrigenId || !form.value.cuentaDestinoId || !form.value.monto) {
    error.value = 'Todos los campos son obligatorios.'
    return
  }

  if (form.value.cuentaOrigenId === form.value.cuentaDestinoId) {
    error.value = 'La cuenta destino no puede ser igual a la cuenta origen.'
    return
  }

  if (form.value.monto <= 0) {
    error.value = 'El monto debe ser mayor a cero.'
    return
  }

  try {
    console.log("➡️ Enviando datos de transferencia:", form.value)

    await api.post(
      '/transferencia/enviar',
      {
        cuentaOrigenId: form.value.cuentaOrigenId,
        cuentaDestinoId: form.value.cuentaDestinoId,
        monto: form.value.monto,
        descripcion: form.value.descripcion // NUEVO
      },
      {
        headers: {
          Authorization: `Bearer ${authStore.token}`
        }
      }
    )

    success.value = '¡Transferencia realizada con éxito!'
    form.value = { cuentaOrigenId: '', cuentaDestinoId: '', monto: '', descripcion: '' }
    await cuentasStore.cargarMisCuentas()
  } catch (err) {
    console.error('Error al transferir:', err)
    error.value = 'No se pudo completar la transferencia.'
  }
}
</script>