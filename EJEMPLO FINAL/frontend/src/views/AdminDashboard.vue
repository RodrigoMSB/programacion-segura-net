<template>
  <div class="container mt-4">
    <h1 class="text-primary mb-4">Panel de Administrador</h1>

    <nav class="mb-4">
      <router-link to="/admin/usuarios" class="me-3">Usuarios</router-link>
      <router-link to="/admin/movimientos" class="me-3">Movimientos</router-link>
      <router-link to="/admin/cuentas">Cuentas</router-link>
    </nav>

    <router-view></router-view>
  </div>
</template>

<script setup>
// No necesitas nada más aquí
</script>