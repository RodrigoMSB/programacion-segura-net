import axios from 'axios'
import { useAuthStore } from './store/auth'

const api = axios.create({
  baseURL: 'https://localhost:5001', // Ajusta al puerto real de tu .NET
})

// Agrega automáticamente el Authorization Header si existe
api.interceptors.request.use((config) => {
  const authStore = useAuthStore()
  if (authStore.token) {
    config.headers.Authorization = `Bearer ${authStore.token}`
  }
  return config
})

export default api