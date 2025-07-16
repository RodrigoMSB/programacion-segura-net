import { defineStore } from 'pinia';
import { ref } from 'vue';
import api from '../axios';
import { useAuthStore } from './auth';

export const useClienteCuentasStore = defineStore('clienteCuentas', () => {
  const cuentas = ref([]);
  const loading = ref(false);
  const error = ref('');

  const authStore = useAuthStore();

  const cargarMisCuentas = async () => {
    loading.value = true;
    error.value = '';

    try {
      const response = await api.get('/cuenta/saldo', {
        headers: {
          Authorization: `Bearer ${authStore.token}`
        }
      });
      cuentas.value = response.data;
    } catch (err) {
      console.error('Error al cargar cuentas:', err);
      error.value = 'No se pudieron cargar las cuentas.';
    } finally {
      loading.value = false;
    }
  };

  return {
    cuentas,
    loading,
    error,
    cargarMisCuentas
  };
});