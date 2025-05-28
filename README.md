# Providers SPA 🧾🌐

**Providers SPA** es una solución full-stack construida con React, .NET 7 y SQL Server para gestionar proveedores y detectar riesgos de contratación mediante cruce automatizado con la lista negra del banco mundial a través de una API.


## 🛠 Tecnologías utilizadas

- **Frontend**: React + Vite
- **Backend**: ASP.NET Core Web API
- **Base de datos**: SQL Server
- **Comunicación**: Fetch API
- **Cruce de datos**: API Risk Scraper (Python + FastAPI)

## 📂 Estructura del proyecto
```
providers-spa/
├── backend/ # API en .NET
│   ├── Controllers/         # Controladores de la API
│   ├── Services/            # Lógica de negocio
│   ├── Models/              # Modelos de datos
│   ├── Data/                # Contexto de base de datos
│   ├── Program.cs           # Punto de entrada de la API
│   ├── appsettings.json     # Configuración general (sin credenciales)
│   └── providers-api.csproj # Proyecto .NET
├── frontend/                # Frontend con React + Vite
│   ├── src/                 # Componentes React
│   ├── public/              # Archivos públicos (favicon, etc.)
│   ├── index.html           # HTML principal
│   ├── package.json         # Dependencias del frontend
│   ├── vite.config.js       # Configuración Vite
│   └── .gitignore           # Ignorar node_modules, etc.
```

## ✨ Funcionalidades clave

- CRUD completo de proveedores desde una interfaz React.
- Persistencia de datos en SQL Server.
- Validación visual de coincidencias con listas del Banco Mundial ([API Risk Scraper](https://github.com/sebasgone/RiskScraper)).
- Panel integrado con búsqueda, actualización, eliminación y cruce (Screening).
- Documentación Swagger de endpoints backend.

## ⚠️ Alcance
 Este proyecto está configurado para correr localmente en modo desarrollo. Para desplegar en producción se recomienda:

- Configurar variables de entorno seguras para cadenas de conexión.
- Usar `dotnet publish` para compilar el backend.
- Usar `npm run build` para generar el frontend en `/dist`.

## 🧠 Configuración del backend

### 1. Clonar el repositorio

```bash
git clone https://github.com/sebasgone/ProvidersManagement.git
```

### 2. Configurar credenciales de servidor SQL Server

Configura dentro del archivo appsettings.json en la carpeta backend/providers-api tu cadena de conexión a tu servidor SQL Server:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=DbSPA;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=True;"
  }
}

```

### 3. Ejecutar migración de inicialización

Ubicate en **backend/providers-api/** y ejecuta el siguiente comando para inicializar la base de datos de proveedores.

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. 📘 Documentación con Swagger
Este backend incluye Swagger UI para explorar y probar los endpoints de la API de manera visual.

Una vez corriendo, acceder a:

```bash
http://localhost:5232/swagger
```

Desde ahí se puede:

- Ver todos los endpoints

- Probar operaciones (GET, POST, PUT, DELETE)

- Ver los esquemas de datos (Provider)

## ⚛️ Configuración de frontend

### 1. Instalación de módulos node 

Ingresar al frontend e instala los módulos necesarios. Ejecutar desde la raíz:

```bash
cd frontend
npm install
```
### 2. Proxy configurado

En **viteconfig.js** se ha configurado un proxy esperando correr el backend en el puerto 5232, el frontend en el 5173/5174 y la API del Banco mundial en el 8000.
Editar el proxy dependiendo del uso de puertos.

## 🚀 Ejecución App

### 1. Ejecutar backend
Desde la raíz del proyecto:

```bash
cd backend/providers-api
dotnet run
```
### 2. Ejecutar API Risk Scraper
La documentación de esta API está en el siguiente [enlace](https://github.com/sebasgone/RiskScraper).

La API debe correr en el puerto 8000.

### 3. Ejecutar frontend integrado
Acceder a frontend y ejecutar:

```bash
npm run dev
```

## ✅ Notas finales

La carpeta node_modules está ignoradas por .gitignore.

Swagger sirve como documentación viva de la API.

El desplegable de países para los diversos procedmientos está en 3 opciones (extensible en futuras versiones).


## ✍️ Autor

Sebastián García Tolentino 
AI Developer
https://www.linkedin.com/in/sebasgone21/

