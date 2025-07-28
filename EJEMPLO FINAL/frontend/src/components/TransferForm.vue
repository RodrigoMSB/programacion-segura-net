<template>
  <div class="container mt-4">
    <div class="row justify-content-center">
      <div class="col-md-6">
        <div class="card shadow-sm">
          <div class="card-header bg-success text-white text-center">
            <h4 class="mb-0">Transferencia Bancaria</h4>
          </div>
          <div class="card-body">
            <form @submit.prevent="enviarTransferencia">
              <div class="mb-3">
                <label for="cuentaDestino" class="form-label">Cuenta Destino</label>
                <input
                  v-model="form.cuentaDestino"
                  type="text"
                  id="cuentaDestino"
                  class="form-control"
                  required
                  placeholder="Ej: 0987654321"
                />
              </div>
              <div class="mb-3">
                <label for="monto" class="form-label">Monto</label>
                <input
                  v-model="form.monto"
                  type="number"
                  id="monto"
                  class="form-control"
                  required
                  min="1"
                />
              </div>
              <div class="mb-3">
                <label for="descripcion" class="form-label">Descripción</label>
                <input
                  v-model="form.descripcion"
                  type="text"
                  id="descripcion"
                  class="form-control"
                  placeholder="Ej: Pago mensual"
                />
              </div>
              <button :disabled="loading" type="submit" class="btn btn-success w-100">
                {{ loading ? 'Enviando...' : 'Enviar Transferencia' }}
              </button>
            </form>

            <p v-if="error" class="text-danger text-center mt-3">
              {{ error }}
            </p>
            <p v-if="success" class="text-success text-center mt-3">
              {{ success }}
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useAuthStore } from '@/store/auth'
import api from '@/axios'

const auth = useAuthStore()

const form = ref({
  cuentaDestino: '',
  monto: '',
  descripcion: ''
})

const loading = ref(false)
const error = ref('')
const success = ref('')

const enviarTransferencia = async () => {
  error.value = ''
  success.value = ''
  loading.value = true
  
  try {
    const token = auth.token
    if (!token) {
      error.value = 'Sesión inválida. Por favor inicie sesión de nuevo.'
      return
    }
    console.log("➡️ Enviando datos de transferencia:", form.value); 
    await api.post('/transferencia/enviar', form.value, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    })
    success.value = 'Transferencia realizada exitosamente.'
    form.value = { cuentaDestino: '', monto: '', descripcion: '' }
  } catch (err) {
    if (err.response?.data) {
      error.value = err.response.data
    } else {
      error.value = 'Error al procesar la transferencia.'
    }
    console.error('Error en la transferencia:', err)
  } finally {
    loading.value = false
  }
}
</script>