# Calculadora de prestamos

Hola! Esta es mi prueba técnica para el puesto de Analista BI/Desarrollador C#

Para correr el proyecto, primero debes se debe correr el comando:

```bash
dotnet ef migrations add InitialCreate
```

Esto creara automáticamente las tablas según los modelos que en este caso son `Prestamo.cs` y `DetallePrestamo.cs`.

Luego ejecutar el comando:

```bash
dotnet restore
```

El programa contiene 2 rutas:

- http://localhost:PORT/Prestamos/Create → Es la ruta del formulario donde se le pide al ‘administrador’ que ingrese los detalles del nuevo préstamo. El plazo de pago de los prestamos debe ser mínimo de 12 meses, y a falta de detalles e investigación, le puse como máximo 36 meses. Al igual que la cantidad a prestar como máximo esta definida en $40k y la mínima en $1001.
- http://localhost:PORT/Prestamos/Details/{id} → Esta es la ruta donde se muestra el detalle del préstamo que se acaba de crear, se puede revisar los otros prestamos creados cambiando el parametro ‘id’ de la URL.

En este caso decidí optar por una interfaz visual simple, pero comprensible y funcional para poder centrarme mayormente en las validaciones y el correcto funcionamiento del backend y la base de datos. 

# ❗❗❗IMPORTANTE ❗❗❗

En mi caso, yo suelo trabajar en Linux, especificamente Ubuntu 24. Esta versión no corre nativamente SQLServer asi que las bases de datos las trabajo con Docker con una imagen de SQLServer simulando un servidor. Por lo que en el archivo `appsettings.json` lo tengo de la siguiente forma:

```bash
{
		"ConnectionStrings": {
		    "DefaultConnection": "Server=localhost,1433;Database=PrestamosDB;User Id=_REPLACE_;Password=_REPLACE_;TrustServerCertificate=True;"
		 } 
  }
```

Para facilitar las cosas, decidí crear un archivo `.bak` con el objetivo de hacer la creación de la BD mucho mas fácil. Solo se necesita irse al DBMS, darle click derecho en ‘Databases’ →‘Restore database’ y seleccionar el archivo PrestamosDB.bak que va adjunto en el repositorio. Deberían haber 8 registros, para no hacer muy pesado el archivo .bak
