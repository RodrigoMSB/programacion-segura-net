<template>
  <div class="container mt-4">
    <h2 class="mb-4">Gestión de Cuentas Bancarias</h2>

    <!-- Alerta de error -->
    <div v-if="cuentasStore.error" class="alert alert-danger">
      {{ cuentasStore.error }}
    </div>

    <!-- Tabla de Cuentas -->
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
    <div v-else class="text-center text-muted">No hay cuentas registradas.</div>

    <!-- Formulario para crear nueva cuenta -->
    <div class="card shadow-sm border-0">
      <div class="card-body">
        <h4 class="card-title mb-3">Crear Nueva Cuenta</h4>
        <form @submit.prevent="crearCuenta">
          <div class="row g-3 align-items-end">
            <div class="col-md-4">
              <label class="form-label">Número de la Cuenta</label>
              <input
                v-model="nuevaCuenta.numeroCuenta"
                type="text"
                class="form-control"
                required
              />
            </div>
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
import { ref, onMounted } from 'vue'
import { useCuentasStore } from '@/store/cuentas'

const cuentasStore = useCuentasStore()

const nuevaCuenta = ref({
  numeroCuenta: '',
  saldo: 0,
  usuarioId: ''
})

const crearCuenta = async () => {
  await cuentasStore.crearCuenta({
    numeroCuenta: nuevaCuenta.value.numeroCuenta,
    saldo: nuevaCuenta.value.saldo,
    usuarioId: parseInt(nuevaCuenta.value.usuarioId)
  })

  nuevaCuenta.value = {
    numeroCuenta: '',
    saldo: 0,
    usuarioId: ''
  }
}

onMounted(() => cuentasStore.cargarCuentas())
</script>