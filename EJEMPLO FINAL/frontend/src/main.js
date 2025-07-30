// Importa la función principal de Vue para crear una instancia de la aplicación
import { createApp } from 'vue'

// Importa el componente raíz de la aplicación (App.vue)
import App from './App.vue'

// Importa el router que define las rutas de la aplicación
import router from './router'

// Importa Pinia, una librería de gestión de estado moderna (reemplazo de Vuex)
import { createPinia } from 'pinia'

// Crea una nueva instancia de la aplicación Vue
const app = createApp(App)

// Agrega el sistema de rutas a la aplicación
app.use(router)

// Agrega Pinia como gestor de estado global
app.use(createPinia())

// Monta la aplicación en el elemento HTML con id="app"
// Esto se refiere al <div id="app"></div> presente en index.html
app.mount('#app')