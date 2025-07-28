// src/store/auth.js
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useAuthStore = defineStore('auth', () => {
  const token = ref(localStorage.getItem('token') || '')
  const role = ref(localStorage.getItem('role') || '')
  const user = ref(null)

  // Intenta cargar el usuario desde localStorage
  try {
    const raw = localStorage.getItem('user')
    if (raw && raw !== 'undefined') {
      user.value = JSON.parse(raw)
    }
  } catch {
    user.value = null
  }

  const setAuth = (nuevoToken, nuevoRole, nuevoUsuario) => {
    token.value = nuevoToken
    role.value = nuevoRole
    user.value = nuevoUsuario

    localStorage.setItem('token', nuevoToken)
    localStorage.setItem('role', nuevoRole)
    localStorage.setItem('user', JSON.stringify(nuevoUsuario))
  }

  const logout = () => {
    token.value = ''
    role.value = ''
    user.value = null
    localStorage.removeItem('token')
    localStorage.removeItem('role')
    localStorage.removeItem('user')
  }

  return {
    token,
    role,
    user,
    setAuth,
    logout
  }
})