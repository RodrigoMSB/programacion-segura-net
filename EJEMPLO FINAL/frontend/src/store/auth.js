import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || '',
    role: localStorage.getItem('role') || '',
  }),
  actions: {
    setAuth(token, role) {
      this.token = token
      this.role = role
      localStorage.setItem('token', token)
      localStorage.setItem('role', role)
    },
    clearAuth() {
      this.token = ''
      this.role = ''
      localStorage.removeItem('token')
      localStorage.removeItem('role')
    }
  }
})