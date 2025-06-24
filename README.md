# 💰 Sistema Bancario - ADO.NET Core Web API
Este proyecto es un sistema bancario desarrollado con ASP.NET Core Web API utilizando ADO.NET para la gestión de una base de datos SQL Server. Permite realizar operaciones CRUD sobre tres entidades clave: Clientes, Cuentas y Transferencias.

    <div align="center"> <img src="https://raw.githubusercontent.com/tuusuario/tu-repo/main/assets/screenshot_home.png" alt="Inicio del sistema" width="80%"> </div>

    
🧠 Características Principales


✔️ Gestión de Clientes (crear, consultar, actualizar, eliminar)
✔️ Administración de Cuentas Bancarias
✔️ Registro y ejecución de Transferencias
✔️ Interfaz gráfica con HTML, CSS y JavaScript puro
✔️ API documentada y probada con Swagger
✔️ Separación por capas (Controller, Service, Model)


⚙️ Tecnologías Utilizadas


ASP.NET Core 6 Web API

ADO.NET (SQL Server)

C#

HTML5 / CSS3

JavaScript Vanilla

Swagger para documentación de la API

Visual Studio 2022

SQL Server Management Studio



🏗️ Estructura del Proyecto


SistemaBancario/


│


├── Controllers/             # Endpoints: Clientes, Cuentas, Transferencias


├── Models/                  # Clases Cliente, Cuenta, Transferencia


├── Services/                # Lógica de negocio implementada en BancoService


├── Data/                    # Clase de conexión a BD (ConexionBD.cs)


│
├── wwwroot/


│   ├── index.html           # Interfaz de usuario principal


│   ├── css/style.css        # Estilos personalizados


│   └── js/app.js            # Lógica del frontend


│
├── Program.cs


└── appsettings.json         # Configuración de la cadena de conexión


🚀 Cómo Ejecutar el Proyecto


Clona el repositorio:

git clone https://github.com/Ismael283-A/SistemaBanco


Configura la cadena de conexión en appsettings.json:

    "ConnectionOptions": {
    "Cadena": "Server=TU_SERVIDOR;Database=BancoDB;Trusted_Connection=True;"
    }
Ejecuta el proyecto desde Visual Studio o terminal:


    dotnet run
Abre Swagger en tu navegador:


     https://localhost:5001/swagger
Abre index.html para acceder al frontend:


    wwwroot/index.html
