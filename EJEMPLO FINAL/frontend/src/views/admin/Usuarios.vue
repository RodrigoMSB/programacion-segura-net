<template>
  <div class="container mt-4">
    <h2 class="mb-3">Gestión de Usuarios</h2>

    <!-- Alerta de error -->
    <div v-if="usuariosStore.error" class="alert alert-danger">
      {{ usuariosStore.error }}
    </div>

    <!-- Loading Spinner -->
    <div v-if="usuariosStore.loading" class="text-center my-4">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>

    <!-- Tabla de Usuarios -->
    <table v-if="!usuariosStore.loading && usuariosStore.usuarios.length" class="table table-bordered table-hover">
      <thead class="table-dark">
        <tr>
          <th>ID</th>
          <th>Nombre</th>
          <th>Email</th>
          <th>Rol</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="usuario in usuariosStore.usuarios" :key="usuario.id">
          <td>{{ usuario.id }}</td>
          <td>{{ usuario.nombre }}</td>
          <td>{{ usuario.email }}</td>
          <td>{{ usuario.rol }}</td>
        </tr>
      </tbody>
    </table>

    <div v-else-if="!usuariosStore.loading && !usuariosStore.usuarios.length" class="text-center text-muted">
      No hay usuarios registrados.
    </div>

    <!-- Formulario para crear nuevo usuario Cliente -->
    <hr class="my-4" />
    <h4>Crear nuevo usuario Cliente</h4>
    <p class="text-muted">
      Solo se solicita nombre y email. El cliente generará su clave luego mediante Registro.
    </p>
    <form @submit.prevent="crearCliente">
      <div class="row g-3">
        <div class="col-md-5">
          <input
            v-model="nuevoCliente.nombre"
            type="text"
            class="form-control"
            placeholder="Nombre completo"
            required
          />
        </div>
        <div class="col-md-5">
          <input
            v-model="nuevoCliente.email"
            type="email"
            class="form-control"
            placeholder="Correo electrónico"
            required
          />
        </div>
        <div class="col-md-2">
          <button type="submit" class="btn btn-success w-100">Crear Cliente</button>
        </div>
      </div>
    </form>
  </div>
</template>

<script setup>
/**
 * Importaciones
 */
import { ref, onMounted } from 'vue'
import { useUsuariosStore } from '@/store/usuarios'

/**
 * Inicializar el store de usuarios
 */
const usuariosStore = useUsuariosStore()

/**
 * Modelo para el nuevo cliente a crear
 */
const nuevoCliente = ref({
  nombre: '',
  email: ''
})

/**
 * Función para crear un nuevo cliente (solo nombre y email)
 */
const crearCliente = async () => {
  await usuariosStore.crearCliente({
    nombre: nuevoCliente.value.nombre,
    email: nuevoCliente.value.email
  });
  // Resetear el formulario
  nuevoCliente.value = { nombre: '', email: '' }
}

/**
 * Al montar el componente, cargar la lista de usuarios
 */
onMounted(() => {
  usuariosStore.cargarUsuarios()
})
</script>