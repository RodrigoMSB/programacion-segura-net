// Importamos Pinia para definir el store y Vue para variables reactivas
import { defineStore } from 'pinia'
import { ref } from 'vue'

// Cliente axios configurado con baseURL y header Authorization automático
import api from '../axios'

// Importamos el store de autenticación para acceder al token JWT
import { useAuthStore } from './auth'

/**
 * Store para gestionar los usuarios desde el panel de Administrador.
 * Incluye:
 *  - Estado reactivo para la lista de usuarios.
 *  - Lógica para cargar todos los usuarios (GET).
 *  - Lógica para crear un nuevo cliente sin contraseña (POST).
 */
export const useUsuariosStore = defineStore('usuarios', () => {
  // ------------------------------
  // Estado reactivo del store
  // ------------------------------

  // Lista de todos los usuarios del sistema
  const usuarios = ref([])

  // Flag de carga para mostrar spinner o desactivar controles
  const loading = ref(false)

  // Texto de error, útil para mostrar en la UI
  const error = ref('')

  // ------------------------------
  // Referencia al store de autenticación
  // ------------------------------
  const authStore = useAuthStore()

  // ------------------------------
  // Cargar todos los usuarios del sistema (uso exclusivo del Admin)
  // ------------------------------
  const cargarUsuarios = async () => {
    loading.value = true
    error.value = ''

    try {
      // Petición protegida al backend con token de administrador
      const response = await api.get('/admin/usuarios', {
        headers: {
          Authorization: `Bearer ${authStore.token}`
        }
      })

      // Se actualiza la lista de usuarios con los datos del backend
      usuarios.value = response.data

    } catch (err) {
      console.error('Error al cargar usuarios:', err)
      error.value = 'No se pudo cargar la lista de usuarios.'
    } finally {
      loading.value = false
    }
  }

  // ------------------------------
  // Crear un nuevo cliente desde el panel de administrador
  // Este cliente NO ingresa contraseña, se espera que la genere después
  // ------------------------------
  const crearCliente = async (nuevoCliente) => {
    loading.value = true
    error.value = ''

    try {
      // Se envía la solicitud POST al endpoint de creación de clientes
      await api.post('/auth/crear-cliente', nuevoCliente, {
        headers: {
          Authorization: `Bearer ${authStore.token}`
        }
      })

      // Si la creación fue exitosa, recargamos la lista
      await cargarUsuarios()

    } catch (err) {
      console.error('Error al crear cliente:', err)
      error.value = 'No se pudo crear el cliente.'
    } finally {
      loading.value = false
    }
  }

  // ------------------------------
  // API pública del store
  // ------------------------------
  return {
    usuarios,
    loading,
    error,
    cargarUsuarios,
    crearCliente
  }
})