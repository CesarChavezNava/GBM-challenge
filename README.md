# GBM Challenge-BackEnd

Una casa de bolsa necesita tener un servicio API para procesar un conjunto de órdenes de compra/venta.

## Pre-requisitos 📋

1. Visual studio 2022 o Visual Studio Code
2. SQL Server
3. .NET 7 [[Descargar](https://dotnet.microsoft.com/en-us/download/dotnet)]
4. Docker
5. Postman (Opcional)

Si se usa visual studio code, es recomendable instalar las siguientes extensiones:

1. C# [[Descargar](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)]
2. C# extensions [[Descargar](https://marketplace.visualstudio.com/items?itemName=kreativ-software.csharpextensions)]
3. Mono Debug [[Descargar](https://marketplace.visualstudio.com/items?itemName=ms-vscode.mono-debug)]
4. Code Runner [[Descargar](https://marketplace.visualstudio.com/items?itemName=formulahendry.code-runner)]

Cambiar el servidor de sql de la cadena de conexión en el archivo `appsettings.development.json` en el proyecto `Broker.Accounts.Infrastructure.API` por el de tu máquina local, si el servidor de sql se llama igual que tu máquina no es necesario cambiarlo.

```
"ConnectionStrings": {
    "AccountConnection": "Server=CAMBIAR_POR_NOMBRE_DE_TU_SERVIDOR;Database=BrokerAccounts;Integrated Security=True;TrustServerCertificate=True"
  }
```

## Comenzando 🛠

### Compilación 🔧

Posicionarse en la carpeta `Broker`

```
cd Broker
```

Una vez posicionado en la carpeta, ejecutar el siguiente comando para compilar el proyecto.

```
dotnet build
```

### Ejecución 🚀

Posicionarse en la carpeta `Broker.Accounts.Infrastructure.API`

```
cd Accounts/Infrastructure/Broker.Accounts.Infrastructure.API
```

Una vez posicionado en la carpeta, ejecutar el siguiente comando para correr la API.

```
dotnet run
```

La aplicación debería estar escuchando en el puerto **5024**, por lo que la URL del servicio es http://localhost:5024/swagger/index.html

## Pruebas ⚙️

Posicionarse en la carpeta `Broker`

```
cd Broker
```

Una vez posicionado en la carpeta, ejecutar el siguiente comando para correr las pruebas unitarias.

```
dotnet test
```

En este momento las pruebas unitarias solo abarcan el dominio.  
Estas pruebas también se ejecutan cada vez que se hace un `push` o `pull request` a la rama `main`, mediante una **git action**, la cual genera un reporte de las pruebas.

## Dockerizacion 📦

Posicionarse en la carpeta `Broker`

```
cd Broker
```

### Crear Imagen 📌

Una vez posicionado en la carpeta `Broker`, ejecutar el siguiente comando generar el contenedor.

```
docker-compose build
```

## Ejecutar Contenedor 📌

Una vez posicionado en la carpeta `Broker`, ejecutar el siguiente comando ejecutar los contenedores.

```
docker-compose up
```

La aplicación debería estar escuchando en el puerto **5024**, por lo que la URL del servicio es http://localhost:5024

Si se tiene instalado postman se pueden importar los archivos que se encuentran en la carpeta `Documents\Postman`, este archivo corresponde a una serie de solicitudes a la API.

## Mas información 🎁

Para conocer más acerca del proyecto en la carpeta `Documents` se encuentra la wiki donde se detalla la construcción del proyecto.
