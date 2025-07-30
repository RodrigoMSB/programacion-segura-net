// Importamos Axios, una librería para realizar peticiones HTTP
import axios from 'axios'

// Importamos el store de autenticación desde Pinia
import { useAuthStore } from './store/auth'

// Creamos una instancia personalizada de Axios
const api = axios.create({
  // Base URL de la API backend — importante: debe coincidir con el puerto real donde corre la API .NET
  baseURL: 'https://localhost:5001',
})

// Agregamos un interceptor que se ejecuta antes de cada request
// Esto permite inyectar dinámicamente el token de autenticación en las cabeceras de las peticiones
api.interceptors.request.use((config) => {
  // Accedemos al store de autenticación, que contiene el token JWT del usuario
  const authStore = useAuthStore()

  // Si el token existe (es decir, el usuario está autenticado),
  // lo agregamos como Authorization Bearer Header a la petición
  if (authStore.token) {
    config.headers.Authorization = `Bearer ${authStore.token}`
  }

  // Retornamos la configuración de la petición con la cabecera modificada (si corresponde)
  return config
})

// Exportamos la instancia personalizada de Axios
// Esto permite reutilizarla en toda la aplicación sin reconfigurarla cada vez
export default api