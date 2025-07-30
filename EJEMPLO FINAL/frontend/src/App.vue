<template>
  <!-- 
    El componente <router-view /> es una directiva especial de Vue Router.
    Actúa como un placeholder dinámico donde se renderiza el componente 
    asociado a la ruta activa.
    
    Es decir: cuando el usuario navega a una ruta, este componente se 
    reemplaza automáticamente por el componente correspondiente.
    
    Ejemplo: si la ruta activa es '/login', aquí se cargará Login.vue
  -->
  <router-view />
</template>

<script setup>
// No es necesario importar nada aquí, ya que este archivo solo funciona
// como layout base para renderizar rutas dinámicamente.
</script>

<style>
/* Este archivo no tiene estilos propios. Los estilos globales están en main.css o vienen desde Bootstrap. */
</style>