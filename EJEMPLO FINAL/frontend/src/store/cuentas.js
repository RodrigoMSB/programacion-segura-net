import { defineStore } from 'pinia'
import { ref } from 'vue'
import api from '../axios'
import { useAuthStore } from './auth'

export const useCuentasStore = defineStore('cuentas', () => {
  const cuentas = ref([])
  const loading = ref(false)
  const error = ref('')
  const authStore = useAuthStore()

  // Cargar todas las cuentas
  const cargarCuentas = async () => {
    loading.value = true
    error.value = ''

    try {
      const response = await api.get('/admin/cuentas', {
        headers: { Authorization: `Bearer ${authStore.token}` }
      })
      cuentas.value = response.data
    } catch (err) {
      console.error('Error al cargar cuentas:', err)
      error.value = 'No se pudo cargar la lista de cuentas.'
    } finally {
      loading.value = false
    }
  }

  // Crear nueva cuenta
  const crearCuenta = async (nuevaCuenta) => {
    loading.value = true
    error.value = ''

    try {
      await api.post('/admin/cuentas', nuevaCuenta, {
        headers: { Authorization: `Bearer ${authStore.token}` }
      })
      await cargarCuentas()
    } catch (err) {
      console.error('Error al crear cuenta:', err)
      error.value = 'No se pudo crear la cuenta.'
    } finally {
      loading.value = false
    }
  }

  return {
    cuentas,
    loading,
    error,
    cargarCuentas,
    crearCuenta
  }
})