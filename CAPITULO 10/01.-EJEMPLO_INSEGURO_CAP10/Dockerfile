# --------------------------------------------------------------------------------------------
# 📄 Dockerfile — Ejemplo INSEGURO para prácticas de despliegue y endurecimiento.
# --------------------------------------------------------------------------------------------

# --------------------------------------------------------------------------------------------
# ❌ MALA PRÁCTICA: Usar imagen base sin versión específica.
# Esto puede derivar en inconsistencias y vulnerabilidades si la imagen cambia inesperadamente.
FROM mcr.microsoft.com/dotnet/aspnet

# --------------------------------------------------------------------------------------------
# 📌 Define el directorio de trabajo dentro del contenedor.
# Todo el contenido se copiará en esta ruta.
WORKDIR /app

# --------------------------------------------------------------------------------------------
# 📌 Copiar todos los archivos del contexto de compilación (directorio local) al contenedor.
# Nota: Sin filtros .dockerignore, se sube TODO, incluyendo archivos innecesarios o sensibles.
COPY . .

# --------------------------------------------------------------------------------------------
# ❌ MALA PRÁCTICA: Ejecutar la aplicación como root.
# No se define un usuario no privilegiado, lo que expone a escalamiento de privilegios
# si un atacante compromete la aplicación dentro del contenedor.
ENTRYPOINT ["dotnet", "InsecureApp.dll"]