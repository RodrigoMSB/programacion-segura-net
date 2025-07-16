<template>
  <div class="container mt-4">
    <h2 class="mb-3">Mis Movimientos</h2>

    <table class="table table-striped table-bordered">
      <thead class="table-dark">
        <tr>
          <th>Fecha</th>
          <th>Cuenta Origen</th>
          <th>Cuenta Destino</th>
          <th>Monto</th>
          <th>Descripción</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>2025-07-13</td>
          <td>1234567890</td>
          <td>0987654321</td>
          <td>$5.000</td>
          <td>Transferencia a Juan</td>
        </tr>
        <tr>
          <td>2025-07-14</td>
          <td>0987654321</td>
          <td>1234567890</td>
          <td>$8.000</td>
          <td>Pago servicios</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
// En el futuro: acá se hace fetch de movimientos del cliente.
</script>