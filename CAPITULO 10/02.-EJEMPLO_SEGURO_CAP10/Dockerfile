# --------------------------------------------------------------------------------------------
# 📄 Dockerfile — Versión SEGURA, aplicando buenas prácticas para imágenes de contenedores.
# --------------------------------------------------------------------------------------------

# ✅ BUENA PRÁCTICA:
# Usar imagen base con versión específica en lugar de una etiqueta genérica 'latest'.
# Esto asegura reproducibilidad y evita que actualizaciones inesperadas rompan el build.
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# --------------------------------------------------------------------------------------------
# 📌 Definir el directorio de trabajo dentro del contenedor.
# Todo el contenido de la aplicación se copiará en esta ruta.
WORKDIR /app

# --------------------------------------------------------------------------------------------
# 📌 Copiar todos los archivos del contexto de compilación al contenedor.
# Nota: Es recomendable usar un archivo `.dockerignore` para excluir archivos innecesarios.
COPY . .

# --------------------------------------------------------------------------------------------
# ✅ BUENA PRÁCTICA:
# Crear un usuario no root dentro de la imagen y asignarle permisos sobre la carpeta de trabajo.
# Esto reduce el impacto de posibles vulnerabilidades al limitar los privilegios de ejecución.
RUN adduser --disabled-password --gecos '' appuser && \
    chown -R appuser /app

# 📌 Cambiar el contexto de ejecución al usuario no root recién creado.
USER appuser

# --------------------------------------------------------------------------------------------
# 📌 Definir el comando de entrada para ejecutar la aplicación.
# Aquí se arranca la aplicación compilada dentro del contenedor.
ENTRYPOINT ["dotnet", "SecureApp.dll"]