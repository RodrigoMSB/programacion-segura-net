<template>
  <div class="container mt-4">
    <h2 class="mb-3">Transferir Fondos</h2>

    <!-- Alerta en caso de error -->
    <div v-if="error" class="alert alert-danger">{{ error }}</div>

    <!-- Alerta en caso de éxito -->
    <div v-if="success" class="alert alert-success">{{ success }}</div>

    <!-- Formulario de transferencia -->
    <form @submit.prevent="realizarTransferencia" novalidate>

      <!-- Cuenta Origen -->
      <div class="mb-3">
        <label for="origen" class="form-label">Cuenta Origen (ID)</label>
        <select v-model="form.cuentaOrigenId" class="form-select" required>
          <option disabled value="">Selecciona tu cuenta</option>
          <!-- Itera sobre las cuentas del usuario -->
          <option v-for="c in cuentasStore.cuentas" :key="c.id" :value="c.id">
            {{ c.numeroCuenta }} - Saldo: ${{ c.saldo.toLocaleString() }}
          </option>
        </select>
        <!-- Muestra el saldo disponible de la cuenta seleccionada -->
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

      <!-- Monto a Transferir -->
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

      <!-- Campo opcional: descripción -->
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

      <!-- Botón de envío -->
      <button type="submit" class="btn btn-primary w-100">Transferir</button>
    </form>
  </div>
</template>

<script setup>
// Importamos funcionalidades de Vue
import { ref, onMounted } from 'vue'

// Axios configurado con interceptores JWT
import api from '@/axios'

// Stores de estado para el cliente autenticado y sus cuentas
import { useClienteCuentasStore } from '@/store/clienteCuentas'
import { useAuthStore } from '@/store/auth'

// Instancia del store de cuentas del cliente
const cuentasStore = useClienteCuentasStore()

// Store con el token del usuario autenticado
const authStore = useAuthStore()

// Formulario reactivo para captura de datos
const form = ref({
  cuentaOrigenId: '',
  cuentaDestinoId: '',
  monto: '',
  descripcion: ''
})

// Estados para mostrar errores y éxitos al usuario
const error = ref('')
const success = ref('')

// Al montar el componente, se cargan las cuentas del usuario desde el backend
onMounted(() => {
  cuentasStore.cargarMisCuentas()
})

// Función para validar y ejecutar la transferencia
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
    // Debug local para ver qué se está enviando
    console.log("➡️ Enviando datos de transferencia:", form.value)

    // Envío de la solicitud POST al backend
    await api.post(
      '/transferencia/enviar',
      {
        cuentaOrigenId: form.value.cuentaOrigenId,
        cuentaDestinoId: form.value.cuentaDestinoId,
        monto: form.value.monto,
        descripcion: form.value.descripcion
      },
      {
        headers: {
          Authorization: `Bearer ${authStore.token}` // Token JWT para autorización
        }
      }
    )

    // Mensaje de éxito
    success.value = '¡Transferencia realizada con éxito!'

    // Limpieza del formulario
    form.value = { cuentaOrigenId: '', cuentaDestinoId: '', monto: '', descripcion: '' }

    // Actualización de saldos
    await cuentasStore.cargarMisCuentas()

  } catch (err) {
    console.error('Error al transferir:', err)
    error.value = 'No se pudo completar la transferencia.'
  }
}
</script>