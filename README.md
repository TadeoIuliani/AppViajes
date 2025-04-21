# AppViajes - Backend

Backend del proyecto **AppViajes**, desarrollado con ASP.NET y Entity Framework, que gestiona vehículos, viajes y ciudades. 
Permite aplicar viajes a destinos con un límite máximo de 10 días y obtiene el estado climático del destino consumiendo la API externa de OpenWeatherMap.

Este backend se conecta además con un frontend desarrollado en React llamado [AppViajesReact](https://github.com/tuusuario/AppViajesReact).
---

## 🚀 Tecnologías utilizadas

- **.NET 8 – Framework principal.**
- **ASP.NET Core Web API – Para crear la API RESTful.**
- **Entity Framework Core – ORM para manipular la base de datos.**
- **SQL Server – Base de datos relacional.**
- **HttpClient – Para consumir API externa de clima.**
- **openweathermap.org – API externa para información del clima.**
- **Swagger - Documentacion de API.**


---

## 📌 Funcionalidades principales

- CRUD completo de:
  - **Vehículos**
  - **Ciudades**
  - **Viajes**
- Asignación de viajes a ciudades con una **duración máxima de 10 días**
- Integración con la **API externa de clima** ([OpenWeatherMap Forecast 30 días](https://openweathermap.org/api/forecast30)):
  - Se obtiene automáticamente el estado del clima del destino al crear o consultar un viaje
- Control de estado de viajes basado condiciones climáticas
- Estructura limpia y modular:
  - Separación entre controladores, servicios y modelos
- Documentación de endpoints con Swagger (si está habilitado)

---

## 🧱 Estructura del proyecto

AppViajesWirsolut/ 
│ 
├── Connected Services/ 
├── Dependencias/ # Paquetes y librerías NuGet 
├── Properties/ # Configuraciones del proyecto 
│ 
├── Context/ # Contexto de la base de datos (DbContext) 
│ └── AppDbContext.cs 
│ 
├── Controllers/ # Controladores de la API 
│ ├── ViajesController.cs 
│ ├── VehiculosController.cs 
│ └── CiudadesController.cs 
│
├── Migrations/ # Archivos de migración de Entity Framework 
│ 
├── Models/ # Entidades del dominio 
│ ├── Viaje.cs 
│ ├── Vehiculo.cs 
│ └── Ciudad.cs 
│ 
├── Services/ # Servicios de lógica de negocio 
│ └── ClimaService.cs │ 
├── appsettings.json # Configuración general del proyecto 
├── AppViajesWirsolut.http # Archivo de pruebas HTTP 
└── Program.cs # Punto de entrada de la aplicación



---

## ⚙️ Instalación y ejecución

### 1. Cloná el repositorio

```bash
git clone https://github.com/tuusuario/AppViajes.git
cd AppViajes
```

### 2. Configurá tu base de datos en appsettings.json
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR_SQL;Database=AppViajesDB;Trusted_Connection=True;"
}
```

### 3. Restaurá paquetes y aplicá migraciones
```bash
dotnet restore
dotnet ef database update
```

### 4. Ejecutá la aplicación
```
dotnet run
```


## 🌦API externa: Clima
Se integra la API de OpenWeatherMap para obtener el estado del clima de la ciudad al momento de crear o consultar un viaje.

URL: https://openweathermap.org/api/forecast30

El clima se consulta automáticamente desde el backend.


## 🛠 Endpoints disponibles:
Swagger disponible en https://localhost:5001/swagger si está habilitado.






