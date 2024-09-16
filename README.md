
# Backend - Wishlist API

Este proyecto es el backend de la aplicación Wishlist. Permite a los usuarios autenticarse, agregar productos a su lista de deseos y manejar sus productos. El backend está construido con **ASP.NET Core** y **Entity Framework** para la interacción con la base de datos.

## Requisitos

Asegúrate de tener instaladas las siguientes herramientas antes de empezar:

- **.NET 6 SDK**: El proyecto utiliza .NET 6 como plataforma principal.
- **SQL Server**: Para la base de datos. Puedes configurarlo de manera local o usar un servidor remoto.
- **Postman** (opcional): Para probar las API manualmente.

## Tecnologías utilizadas

- **.NET 6**: Framework principal para la construcción del backend.
- **Entity Framework Core**: ORM para interactuar con la base de datos SQL Server.
- **ASP.NET Core**: Framework para construir APIs RESTful.
- **Microsoft Identity**: Para la gestión de usuarios, hashing de contraseñas y autenticación.

## Dependencias

El proyecto depende de varios paquetes NuGet. Aquí tienes los más importantes:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.AspNetCore.Authentication.JwtBearer (si decides añadir JWT más adelante)

## Configuración del proyecto

### 1. Clonar el repositorio

Clona el repositorio del proyecto en tu máquina local.

### 2. Configurar la base de datos

Este proyecto utiliza SQL Server. Asegúrate de tener SQL Server corriendo localmente o en un servidor remoto.

1. Crea una nueva base de datos para este proyecto en SQL Server.
2. Configura la conexión a la base de datos en el archivo `appsettings.json`.

#### Ejemplo de configuración en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=WishlistDB;User Id=sa;Password=your_password;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

- **DefaultConnection**: Cambia el valor para reflejar tu servidor de base de datos, nombre de la base de datos, y credenciales.

### 3. Ejecutar las migraciones

El proyecto usa **Entity Framework Core** para manejar las migraciones de la base de datos.

Para aplicar las migraciones a la base de datos y crear las tablas necesarias, utiliza el comando correspondiente en tu CLI preferida.

### 4. Ejecutar el proyecto

Una vez que la base de datos está configurada, puedes ejecutar el proyecto. El servidor correrá en `https://localhost:5001/` o `http://localhost:5000/`.

### 5. Endpoints importantes

Aquí hay una lista de los endpoints más relevantes para la API:

#### Autenticación:

- **POST /auth/login**: Iniciar sesión.
  - **Body**: 
    ```json
    {
      "email": "user@example.com",
      "password": "password123"
    }
    ```

- **POST /auth/registro**: Registrar un nuevo usuario.
  - **Body**: 
    ```json
    {
      "email": "newuser@example.com",
      "password": "password123",
      "nombreUsuario": "NewUser"
    }
    ```

#### Wishlist:

- **GET /deseados/{userId}**: Obtener la lista de deseos del usuario.
- **POST /deseados/agregar**: Agregar un producto a la wishlist.
  - **Body**:
    ```json
    {
      "userId": 1,
      "productoId": 12
    }
    ```

- **DELETE /deseados/eliminar**: Eliminar un producto de la wishlist.
  - **Body**:
    ```json
    {
      "userId": 1,
      "productoId": 12
    }
    ```

### 6. Probar la API

Puedes utilizar **Postman** o **cURL** para probar los endpoints del API.

### 7. Ejecutar pruebas (opcional)

Si tienes pruebas unitarias o de integración configuradas en el proyecto, puedes ejecutarlas con el comando correspondiente en tu CLI.

## Errores comunes

- **Error de conexión con la base de datos**: Verifica que la cadena de conexión en `appsettings.json` sea correcta y que el servidor de SQL esté en funcionamiento.
- **Errores de migración**: Si tienes problemas con las migraciones, asegúrate de que el proyecto tiene las dependencias de **Entity Framework Core** correctamente instaladas y que la base de datos está accesible.

## Contribuir

Si deseas contribuir al proyecto, por favor:

1. Haz un fork del repositorio.
2. Crea una nueva rama con tu funcionalidad o corrección de errores.
3. Haz un pull request a la rama principal del proyecto.
