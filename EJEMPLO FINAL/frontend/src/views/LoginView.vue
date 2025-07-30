<template>
  <!-- Contenedor principal centrado -->
  <div class="container mt-5">
    <div class="row justify-content-center">
      <div class="col-md-6 col-lg-4">
        <!-- Tarjeta visual Bootstrap -->
        <div class="card shadow">
          <!-- Encabezado de la tarjeta -->
          <div class="card-header text-center bg-primary text-white">
            <h3 class="mb-0">Iniciar Sesión</h3>
          </div>

          <!-- Cuerpo del formulario -->
          <div class="card-body">
            <form @submit.prevent="handleLogin"> <!-- Previene comportamiento por defecto -->
              <!-- Campo para el email -->
              <div class="mb-3">
                <label for="email" class="form-label">Email</label>
                <input
                  v-model="email"
                  type="email"
                  id="email"
                  class="form-control"
                  placeholder="correo@banco.com"
                  required
                />
              </div>

              <!-- Campo para la contraseña -->
              <div class="mb-3">
                <label for="password" class="form-label">Contraseña</label>
                <input
                  v-model="password"
                  type="password"
                  id="password"
                  class="form-control"
                  placeholder="******"
                  required
                />
              </div>

              <!-- Botón para enviar el formulario -->
              <button type="submit" class="btn btn-success w-100">
                Ingresar
              </button>
            </form>

            <!-- Mensaje de error si existe -->
            <p v-if="error" class="text-danger text-center mt-3">
              {{ error }}
            </p>
          </div>

          <!-- Pie de la tarjeta -->
          <div class="card-footer text-muted text-center small">
            © Banco Seguro App 2025
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
// Importamos los recursos necesarios de Vue, el router, store, y servicios
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../store/auth'
import api from '../axios'                   // Cliente Axios configurado
import { jwtDecode } from 'jwt-decode'       // Librería para decodificar el token JWT

// Definición de variables reactivas para el formulario
const email = ref('')
const password = ref('')
const error = ref('')
const loading = ref(false)                   // Indicador de carga durante la petición
const router = useRouter()
const authStore = useAuthStore()

// Validación simple del formato del correo electrónico
function validateEmail(email) {
  const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
  return re.test(email)
}

// Función principal para manejar el login
const handleLogin = async () => {
  error.value = ''  // Limpiamos errores anteriores

  // Validación local del correo
  if (!validateEmail(email.value)) {
    error.value = 'El correo ingresado no tiene formato válido.'
    return
  }

  loading.value = true

  try {
    // Petición HTTP al backend para obtener token
    const response = await api.post('/auth/login', {
      email: email.value,
      password: password.value
    })

    const token = response.data.token

    // Decodificamos el token para obtener los datos del usuario
    const decoded = jwtDecode(token)
    console.log('Token decodificado:', decoded)

    // Construimos el objeto de usuario desde las claims del token
    const usuario = {
      id: decoded.sub,
      nombre: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
      email: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"],
      rol: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
    }

    // Guardamos el token y el usuario en el store
    authStore.setAuth(token, usuario.rol, usuario)

    // Redireccionamos según el rol del usuario
    if (usuario.rol === 'Admin') {
      router.push('/admin')
    } else {
      router.push('/cliente')
    }

  } catch (err) {
    console.error('Error al intentar logear:', err)

    // Gestión de errores según el tipo de respuesta
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