<template>
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-6 col-lg-4">
        <div class="card shadow">
          <div class="card-header text-center bg-primary text-white">
            <h3 class="mb-0">Iniciar Sesión</h3>
          </div>
          <div class="card-body">
            <form @submit.prevent="handleLogin">
              <div class="mb-3">
                <label for="email" class="form-label">Email</label>
                <input v-model="email" type="email" id="email" class="form-control" placeholder="correo@banco.com"
                  required />
              </div>
              <div class="mb-3">
                <label for="password" class="form-label">Contraseña</label>
                <input v-model="password" type="password" id="password" class="form-control" placeholder="******"
                  required />
              </div>
              <button type="submit" class="btn btn-success w-100">
                Ingresar
              </button>
            </form>
            <p v-if="error" class="text-danger text-center mt-3">
              {{ error }}
            </p>
          </div>
          <div class="card-footer text-muted text-center small">
            © Banco Seguro App 2025
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../store/auth'
import api from '../axios'
import { jwtDecode } from 'jwt-decode';

const email = ref('')
const password = ref('')
const error = ref('')
const router = useRouter()
const authStore = useAuthStore()
const loading = ref(false)

function validateEmail(email) {
  const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
  return re.test(email)
}

const handleLogin = async () => {
  error.value = ''

  if (!validateEmail(email.value)) {
    error.value = 'El correo ingresado no tiene formato válido.'
    return
  }

  loading.value = true

  try {
    const response = await api.post('/auth/login', {
      email: email.value,
      password: password.value
    })

    const token = response.data.token
    const decoded = jwtDecode(token);
    console.log('Token decodificado:', decoded);
    // Extraer rol desde claim estandar de Microsoft
    const rol = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    console.log('ROL extraído:', rol);
    authStore.setAuth(token, rol)

    if (rol === 'Admin') {
      router.push('/admin')
    } else {
      router.push('/cliente')
    }

  } catch (err) {
    console.error('Error al intentar logear:', err)
    if (err.response?.status === 401) {
      error.value = 'Credenciales inválidas. Verifica tus datos.'
    } else if (err.message.includes('Network')) {
      error.value = 'No se pudo conectar al servidor. Revisa tu conexión.'
    } else {
      error.value = 'Error inesperado en el servidor.'
    }
  } finally {
    loading.value = false
  }
}

</script>