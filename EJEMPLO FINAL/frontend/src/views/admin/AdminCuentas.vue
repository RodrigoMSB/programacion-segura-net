<template>
  <div class="container mt-4">
    <h2 class="mb-3">Gestión de Cuentas Bancarias</h2>

    <!-- Alerta de error -->
    <div v-if="cuentasStore.error" class="alert alert-danger">
      {{ cuentasStore.error }}
    </div>

    <!-- Tabla de Cuentas -->
    <table class="table table-bordered table-hover" v-if="cuentasStore.cuentas.length">
      <thead class="table-dark">
        <tr>
          <th>ID</th>
          <th>Número de Cuenta</th>
          <th>Saldo</th>
          <th>Usuario ID</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="c in cuentasStore.cuentas" :key="c.id">
          <td>{{ c.id }}</td>
          <td>{{ c.numeroCuenta }}</td>
          <td>{{ c.saldo }}</td>
          <td>{{ c.usuarioId }}</td>
        </tr>
      </tbody>
    </table>

    <div v-else class="text-center text-muted">No hay cuentas registradas.</div>

    <hr class="my-4" />

    <h4>Crear Nueva Cuenta</h4>
    <form @submit.prevent="crearCuenta">
      <div class="row g-3">
        <div class="col-md-4">
          <input
            v-model="nuevaCuenta.numeroCuenta"
            type="text"
            class="form-control"
            placeholder="Número de cuenta"
            required
          />
        </div>
        <div class="col-md-3">
          <input
            v-model="nuevaCuenta.saldo"
            type="number"
            min="0"
            step="0.01"
            class="form-control"
            placeholder="Saldo inicial"
            required
          />
        </div>
        <div class="col-md-3">
          <input
            v-model="nuevaCuenta.usuarioId"
            type="number"
            min="1"
            class="form-control"
            placeholder="ID de usuario"
            required
          />
        </div>
        <div class="col-md-2">
          <button type="submit" class="btn btn-success w-100">
            Crear Cuenta
          </button>
        </div>
      </div>
    </form>
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