<template>
  <!-- Contenedor principal con espaciado vertical -->
  <div class="container py-5">
    <div class="row justify-content-center">
      <div class="col-md-5">
        <!-- Tarjeta visual para centrar el formulario -->
        <div class="card shadow-sm">
          <div class="card-body">
            <h3 class="card-title text-center mb-4">Registro</h3>

            <!-- Alerta en caso de error -->
            <div v-if="error" class="alert alert-danger">
              {{ error }}
            </div>

            <!-- Alerta en caso de éxito -->
            <div v-if="success" class="alert alert-success">
              {{ success }}
            </div>

            <!-- Formulario de registro de usuario -->
            <form @submit.prevent="registrar">
              <!-- Campo para el nombre -->
              <div class="mb-3">
                <label for="nombre" class="form-label">Nombre completo</label>
                <input
                  type="text"
                  v-model="form.nombre"
                  id="nombre"
                  class="form-control"
                  placeholder="Ej: Juan Pérez"
                  required
                />
              </div>

              <!-- Campo para el correo electrónico -->
              <div class="mb-3">
                <label for="email" class="form-label">Correo electrónico</label>
                <input
                  type="email"
                  v-model="form.email"
                  id="email"
                  class="form-control"
                  placeholder="Ej: juan@correo.com"
                  required
                />
              </div>

              <!-- Campo para la contraseña -->
              <div class="mb-3">
                <label for="password" class="form-label">Contraseña</label>
                <input
                  type="password"
                  v-model="form.password"
                  id="password"
                  class="form-control"
                  placeholder="Mínimo 6 caracteres"
                  required
                />
              </div>

              <!-- Botón de envío, deshabilitado mientras carga -->
              <button
                type="submit"
                class="btn btn-primary w-100"
                :disabled="loading"
              >
                {{ loading ? 'Registrando...' : 'Crear cuenta' }}
              </button>
            </form>

            <!-- Enlace para usuarios que ya tienen cuenta -->
            <div class="text-center mt-3">
              <span>¿Ya tienes cuenta? </span>
              <router-link to="/login">Inicia sesión aquí</router-link>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
// Importamos funciones de Vue y librerías necesarias
import { ref } from 'vue'                // Variables reactivas
import { useRouter } from 'vue-router'   // Para redirección
import axios from 'axios'                // Para llamadas HTTP

// Instanciamos router para redirección tras registro
const router = useRouter()

// Datos del formulario
const form = ref({
  nombre: '',
  email: '',
  password: '',
  rol: '',
})

// Estado de mensajes y carga
const error = ref('')
const success = ref('')
const loading = ref(false)

// Función de registro
const registrar = async () => {
  error.value = ''
  success.value = ''

  // Validación básica
  if (form.value.password.length < 6) {
    error.value = 'La contraseña debe tener al menos 6 caracteres.'
    return
  }

  loading.value = true

  try {
    // Petición POST al backend
    await axios.post('https://localhost:5001/auth/completar-registro', form.value)

    success.value = 'Usuario registrado correctamente. Redirigiendo al login...'
    form.value = { nombre: '', email: '', password: '', rol: '' }

    setTimeout(() => router.push('/login'), 1500)
  } catch (err) {
    error.value = err.response?.data || 'Error al registrar usuario.'
  } finally {
    loading.value = false
  }
}
</script>