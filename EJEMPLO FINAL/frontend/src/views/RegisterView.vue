<template>
  <div class="container py-5">
    <div class="row justify-content-center">
      <div class="col-md-5">
        <div class="card shadow-sm">
          <div class="card-body">
            <h3 class="card-title text-center mb-4">Registro</h3>

            <!-- Mensaje de error -->
            <div v-if="error" class="alert alert-danger">
              {{ error }}
            </div>

            <!-- Mensaje de éxito -->
            <div v-if="success" class="alert alert-success">
              {{ success }}
            </div>

            <form @submit.prevent="registrar">
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

              <div class="mb-3">
                <label for="rol" class="form-label">Rol</label>
                <select v-model="form.rol" id="rol" class="form-select" required>
                  <option disabled value="">Seleccione un rol</option>
                  <option value="Admin">Administrador</option>
                  <option value="Cliente">Cliente</option>
                </select>
              </div>

              <button
                type="submit"
                class="btn btn-primary w-100"
                :disabled="loading"
              >
                {{ loading ? 'Registrando...' : 'Crear cuenta' }}
              </button>
            </form>

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
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';

const router = useRouter();
const form = ref({
  nombre: '',
  email: '',
  password: '',
  rol: '',
});
const error = ref('');
const success = ref('');
const loading = ref(false);

const registrar = async () => {
  error.value = '';
  success.value = '';

  // Validación básica extra antes de enviar
  if (form.value.password.length < 6) {
    error.value = 'La contraseña debe tener al menos 6 caracteres.';
    return;
  }

  loading.value = true;

  try {
    await axios.post('https://localhost:5001/auth/completar-registro', form.value);

    // Feedback al usuario
    success.value = 'Usuario registrado correctamente. Redirigiendo al login...';

    // Limpieza del formulario
    form.value = { nombre: '', email: '', password: '', rol: '' };

    // Redirección tras pequeño delay
    setTimeout(() => router.push('/login'), 1500);
  } catch (err) {
    if (err.response?.data) {
      error.value = err.response.data;
    } else {
      error.value = 'Error al registrar usuario.';
    }
  } finally {
    loading.value = false;
  }
};
</script>