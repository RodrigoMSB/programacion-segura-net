// Importamos la función para definir un store con Pinia
import { defineStore } from 'pinia'
// Importamos la API de composición reactiva de Vue
import { ref } from 'vue'

// Creamos el store 'auth', que es utilizado en toda la app para controlar login y logout
export const useAuthStore = defineStore('auth', () => {
  // Almacena el token JWT. Se intenta cargar desde localStorage si existe
  const token = ref(localStorage.getItem('token') || '')

  // Almacena el rol del usuario ('Admin' o 'Cliente'), también persistido
  const role = ref(localStorage.getItem('role') || '')

  // Objeto usuario completo (id, nombre, email, etc.)
  const user = ref(null)

  // Intentamos cargar los datos del usuario desde localStorage (en formato JSON)
  try {
    const raw = localStorage.getItem('user')
    if (raw && raw !== 'undefined') {
      user.value = JSON.parse(raw)
    }
  } catch {
    // En caso de error (JSON inválido), se fuerza a null
    user.value = null
  }

  // 🔑 Función para establecer datos de autenticación luego de hacer login
  const setAuth = (nuevoToken, nuevoRole, nuevoUsuario) => {
    token.value = nuevoToken
    role.value = nuevoRole
    user.value = nuevoUsuario

    // Persistimos en localStorage para mantener sesión entre recargas
    localStorage.setItem('token', nuevoToken)
    localStorage.setItem('role', nuevoRole)
    localStorage.setItem('user', JSON.stringify(nuevoUsuario))
  }

  // 🚪 Cierra sesión: borra token, rol y usuario del store y del localStorage
  const logout = () => {
    token.value = ''
    role.value = ''
    user.value = null

    localStorage.removeItem('token')
    localStorage.removeItem('role')
    localStorage.removeItem('user')
  }

  // Exportamos lo necesario para usar este store desde cualquier componente
  return {
    token,
    role,
    user,
    setAuth,
    logout
  }
})