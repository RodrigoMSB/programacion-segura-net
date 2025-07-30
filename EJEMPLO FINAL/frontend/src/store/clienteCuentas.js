// Definimos un store usando Pinia (reemplazo moderno de Vuex)
import { defineStore } from 'pinia'

// Importamos ref para declarar variables reactivas
import { ref } from 'vue'

// Importamos nuestro cliente Axios ya configurado con interceptores
import api from '../axios'

// Importamos el store de autenticación para acceder al token JWT
import { useAuthStore } from './auth'

// Definimos el store 'clienteCuentas', el cual se puede usar en cualquier componente
export const useClienteCuentasStore = defineStore('clienteCuentas', () => {
  // Arreglo que contiene las cuentas del usuario actual
  const cuentas = ref([])

  // Bandera de carga (útil para mostrar spinners u ocultar contenido)
  const loading = ref(false)

  // Variable para guardar mensajes de error si ocurre alguna falla
  const error = ref('')

  // Accedemos al store de autenticación para obtener el token de sesión actual
  const authStore = useAuthStore()

  // Función asincrónica que consulta las cuentas del usuario autenticado
  const cargarMisCuentas = async () => {
    loading.value = true
    error.value = ''

    try {
      // Llamamos al backend incluyendo el token JWT en el header
      const response = await api.get('/cuenta/mis-cuentas', {
        headers: {
          Authorization: `Bearer ${authStore.token}`
        }
      })

      // Almacenamos la respuesta del backend en el array de cuentas
      cuentas.value = response.data
    } catch (err) {
      console.error('Error al cargar cuentas:', err)
      error.value = 'No se pudieron cargar las cuentas.'
    } finally {
      // Ocultamos el estado de carga aunque haya fallado
      loading.value = false
    }
  }

  // Exponemos las variables y métodos del store
  return {
    cuentas,
    loading,
    error,
    cargarMisCuentas
  }
})