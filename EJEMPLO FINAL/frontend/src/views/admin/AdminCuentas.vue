<template>
  <div class="container mt-4">
    <h2 class="mb-4">Gestión de Cuentas Bancarias</h2>

    <!-- Alerta en caso de error al cargar o crear cuentas -->
    <div v-if="cuentasStore.error" class="alert alert-danger">
      {{ cuentasStore.error }}
    </div>

    <!-- Tabla con cuentas existentes (solo si hay registros) -->
    <div class="table-responsive mb-4" v-if="cuentasStore.cuentas.length">
      <table class="table table-bordered table-hover align-middle text-center">
        <thead class="table-primary">
          <tr>
            <th>ID</th>
            <th>Número de Cuenta</th>
            <th>Saldo</th>
            <th>ID de Usuario</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="c in cuentasStore.cuentas" :key="c.id">
            <td>{{ c.id }}</td>
            <td>{{ c.numeroCuenta }}</td>
            <td>${{ c.saldo.toLocaleString() }}</td>
            <td>{{ c.usuarioId }}</td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Mensaje cuando no hay cuentas cargadas -->
    <div v-else class="text-center text-muted">
      No hay cuentas registradas.
    </div>

    <!-- Formulario para crear una nueva cuenta -->
    <div class="card shadow-sm border-0">
      <div class="card-body">
        <h4 class="card-title mb-3">Crear Nueva Cuenta</h4>
        <form @submit.prevent="crearCuenta">
          <div class="row g-3 align-items-end">
            <!-- Campo: Número de Cuenta -->
            <div class="col-md-4">
              <label class="form-label">Número de la Cuenta</label>
              <input
                v-model="nuevaCuenta.numeroCuenta"
                type="text"
                class="form-control"
                required
              />
            </div>

            <!-- Campo: Saldo Inicial -->
            <div class="col-md-3">
              <label class="form-label">Monto Inicial (Saldo)</label>
              <input
                v-model="nuevaCuenta.saldo"
                type="number"
                min="0"
                step="0.01"
                class="form-control"
                required
              />
            </div>

            <!-- Campo: ID del Usuario -->
            <div class="col-md-3">
              <label class="form-label">ID del Usuario</label>
              <input
                v-model="nuevaCuenta.usuarioId"
                type="number"
                min="1"
                class="form-control"
                required
              />
            </div>

            <!-- Botón: Crear cuenta -->
            <div class="col-md-2">
              <button type="submit" class="btn btn-success w-100">
                <i class="bi bi-plus-circle me-1"></i> Crear
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup>
// Importamos herramientas de Vue
import { ref, onMounted } from 'vue'

// Importamos el store centralizado de cuentas (Pinia)
import { useCuentasStore } from '@/store/cuentas'

// Referencia al store donde están las cuentas y lógica asociada
const cuentasStore = useCuentasStore()

// Estado local reactivo para almacenar la cuenta que se está creando
const nuevaCuenta = ref({
  numeroCuenta: '', // número ingresado por el administrador
  saldo: 0,          // saldo inicial
  usuarioId: ''      // ID del usuario al que se le asignará la cuenta
})

// Función que invoca al store para crear una nueva cuenta
const crearCuenta = async () => {
  await cuentasStore.crearCuenta({
    numeroCuenta: nuevaCuenta.value.numeroCuenta,
    saldo: nuevaCuenta.value.saldo,
    usuarioId: parseInt(nuevaCuenta.value.usuarioId)
  })

  // Reinicia el formulario luego de crear
  nuevaCuenta.value = {
    numeroCuenta: '',
    saldo: 0,
    usuarioId: ''
  }
}

// Al montar el componente, se cargan las cuentas ya existentes desde el backend
onMounted(() => cuentasStore.cargarCuentas())
</script>