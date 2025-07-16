// src/store/usuarios.js

import { defineStore } from 'pinia'
import { ref } from 'vue'
import api from '../axios'
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
  // Estado
  // ------------------------------
  const usuarios = ref([])
  const loading = ref(false)
  const error = ref('')

  // ------------------------------
  // Referencia al AuthStore
  // ------------------------------
  const authStore = useAuthStore()

  // ------------------------------
  // Cargar todos los usuarios
  // ------------------------------
  const cargarUsuarios = async () => {
    loading.value = true
    error.value = ''

    try {
      // Enviar solicitud al backend con token JWT
      const response = await api.get('/admin/usuarios', {
        headers: {
          Authorization: `Bearer ${authStore.token}`
        }
      })
      usuarios.value = response.data
    } catch (err) {
      console.error('Error al cargar usuarios:', err)
      error.value = 'No se pudo cargar la lista de usuarios.'
    } finally {
      loading.value = false
    }
  }

  // ------------------------------
  // Crear un nuevo cliente (sin contraseña)
  // ------------------------------
  const crearCliente = async (nuevoCliente) => {
    loading.value = true
    error.value = ''

    try {
      await api.post('/auth/crear-cliente', nuevoCliente, {
        headers: {
          Authorization: `Bearer ${authStore.token}`
        }
      })
      // Actualiza la lista después de crear
      await cargarUsuarios()
    } catch (err) {
      console.error('Error al crear cliente:', err)
      error.value = 'No se pudo crear el cliente.'
    } finally {
      loading.value = false
    }
  }

  // ------------------------------
  // Return API del Store
  // ------------------------------
  return {
    usuarios,
    loading,
    error,
    cargarUsuarios,
    crearCliente
  }
})