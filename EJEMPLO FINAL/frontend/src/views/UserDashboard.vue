<template>
  <div class="container mt-4">
    <h2 class="text-success mb-4">Panel del Cliente</h2>

    <nav class="nav nav-pills mb-4">
      <RouterLink class="nav-link" to="/cliente/cuentas">Mis Cuentas</RouterLink>
      <RouterLink class="nav-link" to="/cliente/movimientos">Movimientos</RouterLink>
      <RouterLink class="nav-link" to="/cliente/transferir">Transferir</RouterLink>
    </nav>

    <router-view></router-view>
  </div>
</template>

<script setup>
import { RouterLink, RouterView } from 'vue-router';
</script>