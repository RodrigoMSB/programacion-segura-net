<template>
  <div class="container mt-4">
    <h2 class="mb-4">Mis Cuentas Bancarias</h2>

    <div v-if="cuentasStore.loading" class="text-center my-4">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Cargando...</span>
      </div>
    </div>

    <div v-if="cuentasStore.error" class="alert alert-danger">
      {{ cuentasStore.error }}
    </div>

    <table v-if="!cuentasStore.loading && cuentasStore.cuentas.length" class="table table-bordered table-hover">
      <thead class="table-dark">
        <tr>
          <th>Número de Cuenta</th>
          <th>Saldo Disponible</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="cuenta in cuentasStore.cuentas" :key="cuenta.numeroCuenta">
          <td>{{ cuenta.numeroCuenta }}</td>
          <td>${{ cuenta.saldo.toLocaleString() }}</td>
          <td>
            <button class="btn btn-primary btn-sm">
              Ver Movimientos
            </button>
          </td>
        </tr>
      </tbody>
    </table>

    <div v-else-if="!cuentasStore.loading && !cuentasStore.cuentas.length" class="text-center text-muted">
      No tienes cuentas bancarias asociadas.
    </div>
  </div>
</template>

<script setup>
import { onMounted } from 'vue';
import { useClienteCuentasStore } from '@/store/clienteCuentas';

const cuentasStore = useClienteCuentasStore();

onMounted(() => {
  cuentasStore.cargarMisCuentas();
});
</script>