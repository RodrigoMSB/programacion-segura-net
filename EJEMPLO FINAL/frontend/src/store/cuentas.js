// Definimos un store global usando Pinia (composición moderna y modular para estado)
import { defineStore } from 'pinia'

// Importamos `ref` de Vue para variables reactivas
import { ref } from 'vue'

// Cliente Axios centralizado con configuración e interceptores
import api from '../axios'

// Importamos el store de autenticación para acceder al token JWT del usuario logeado
import { useAuthStore } from './auth'

// Definimos un store llamado 'cuentas' para gestionar todas las cuentas del sistema
export const useCuentasStore = defineStore('cuentas', () => {
  // Estado reactivo que contiene la lista de cuentas bancarias
  const cuentas = ref([])

  // Bandera de carga, útil para mostrar un spinner o deshabilitar controles
  const loading = ref(false)

  // Mensaje de error que puede ser mostrado en la UI
  const error = ref('')

  // Obtenemos el store de autenticación para acceder al token actual
  const authStore = useAuthStore()

  // Método asincrónico para obtener TODAS las cuentas del sistema
  const cargarCuentas = async () => {
    loading.value = true
    error.value = ''

    try {
      // Llamada al backend protegida con token de administrador
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

  // Método para crear una nueva cuenta desde el panel administrativo
  const crearCuenta = async (nuevaCuenta) => {
    loading.value = true
    error.value = ''

    try {
      // Enviamos los datos de la nueva cuenta al endpoint protegido
      await api.post('/admin/cuentas', nuevaCuenta, {
        headers: { Authorization: `Bearer ${authStore.token}` }
      })

      // Si todo fue exitoso, recargamos la lista para reflejar el cambio
      await cargarCuentas()
    } catch (err) {
      console.error('Error al crear cuenta:', err)
      error.value = 'No se pudo crear la cuenta.'
    } finally {
      loading.value = false
    }
  }

  // Exponemos las variables y funciones del store
  return {
    cuentas,
    loading,
    error,
    cargarCuentas,
    crearCuenta
  }
})