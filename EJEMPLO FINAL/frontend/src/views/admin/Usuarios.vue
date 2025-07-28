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

    <!-- Tabla de usuarios -->
    <table
      v-if="!usuariosStore.loading && usuariosStore.usuarios.length"
      class="table table-bordered table-hover"
    >
      <thead class="table-primary">
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

    <div
      v-else-if="!usuariosStore.loading && !usuariosStore.usuarios.length"
      class="text-center text-muted"
    >
      No hay usuarios registrados.
    </div>

    <!-- Formulario crear cliente -->
    <div class="card p-4 mt-4 shadow-sm">
      <h4 class="mb-3">Crear Nuevo Cliente</h4>
      <p class="text-muted mb-4">
        Solo se solicita nombre y correo. El cliente generará su contraseña posteriormente mediante su propio registro.
      </p>
      <form @submit.prevent="crearCliente">
        <div class="row g-3">
          <div class="col-md-5">
            <label class="form-label">Nombre completo</label>
            <input
              v-model="nuevoCliente.nombre"
              type="text"
              class="form-control"
              required
            />
          </div>
          <div class="col-md-5">
            <label class="form-label">Correo electrónico</label>
            <input
              v-model="nuevoCliente.email"
              type="email"
              class="form-control"
              required
            />
          </div>
          <div class="col-md-2 d-flex align-items-end">
            <button type="submit" class="btn btn-success w-100">
              Crear
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useUsuariosStore } from '@/store/usuarios'

const usuariosStore = useUsuariosStore()

const nuevoCliente = ref({
  nombre: '',
  email: ''
})

const crearCliente = async () => {
  await usuariosStore.crearCliente({
    nombre: nuevoCliente.value.nombre,
    email: nuevoCliente.value.email
  })
  nuevoCliente.value = { nombre: '', email: '' }
}

onMounted(() => {
  usuariosStore.cargarUsuarios()
})
</script>