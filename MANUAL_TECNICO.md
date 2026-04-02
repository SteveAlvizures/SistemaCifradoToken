# Manual técnico

## SistemaCifradoToken

Este manual técnico describe la estructura general del proyecto, las tecnologías utilizadas, la base de datos y los pasos básicos para ejecutar la aplicación.

---

## 1. Descripción general

**SistemaCifradoToken** es una aplicación web desarrollada para cifrar textos, almacenarlos en una base de datos y generar un token para su posterior consulta.

El sistema fue construido como una aplicación web en **ASP.NET Core MVC**, utilizando **C#** como lenguaje principal y **SQL Server** como gestor de base de datos.

---

## 2. Tecnologías utilizadas

- Lenguaje de programación: **C#**
- Framework: **ASP.NET Core MVC**
- Base de datos: **Microsoft SQL Server**
- Entorno de desarrollo: **Visual Studio**
- Control de versiones: **GitHub**

---

## 3. Estructura del proyecto

El proyecto está organizado en varias carpetas y archivos principales:

### Controllers
Contiene la lógica de navegación y control del sistema.

Archivos principales:
- `HomeController.cs`
- `UsuariosController.cs`
- `MensajesController.cs`
- `HistorialController.cs`

### Models
Contiene las clases principales del sistema.

Archivos principales:
- `Usuario.cs`
- `Mensaje.cs`
- `HistorialAcceso.cs`
- `ConsultaTokenViewModel.cs`
- `ApplicationDbContext.cs`

### Services
Contiene la lógica de cifrado y descifrado.

Archivo principal:
- `CifradoService.cs`

### Views
Contiene las vistas del sistema.

Vistas principales:
- `Views/Home/Index.cshtml`
- `Views/Usuarios/Registro.cshtml`
- `Views/Mensajes/Crear.cshtml`
- `Views/Mensajes/Lista.cshtml`
- `Views/Mensajes/ConsultarToken.cshtml`
- `Views/Historial/Lista.cshtml`

### wwwroot
Contiene archivos estáticos del proyecto.

### appsettings.json
Contiene la cadena de conexión a la base de datos.

### Program.cs
Contiene la configuración principal de arranque del sistema.

---

## 4. Base de datos

La base de datos utilizada se llama:

`SistemaCifradoTokenDB`

Las tablas principales son:

### Usuarios
Guarda la información de los usuarios registrados.

Campos principales:
- IdUsuario
- Nombre
- Correo
- NombreUsuario
- PasswordHash
- Estado
- FechaCreacion

### Mensajes
Guarda los mensajes cifrados y los datos relacionados.

Campos principales:
- IdMensaje
- IdUsuarioPropietario
- TextoCifrado
- HashIntegridad
- Token
- Etiqueta
- Estado
- FechaCreacion
- FechaPrimerUso
- FechaEliminacion
- TotalIntentos
- TotalExitosos

### HistorialAccesos
Guarda el historial de intentos de consulta.

Campos principales:
- IdHistorial
- IdMensaje
- IdUsuarioAccion
- TokenIngresado
- Resultado
- Motivo
- DireccionIP
- UserAgent
- Dispositivo
- FechaHora

---

## 5. Funcionalidad principal

### Registro de usuarios
Permite crear nuevos usuarios en el sistema.

### Creación de mensajes
Permite ingresar un texto, cifrarlo y almacenarlo en la base de datos.

### Generación de token
Cada mensaje nuevo genera un token único.

### Consulta por token
Permite buscar un mensaje usando el token y mostrar su contenido descifrado.

### Historial de accesos
Registra consultas exitosas y fallidas.

### Eliminación lógica
Permite marcar mensajes como eliminados sin borrarlos físicamente de la base de datos.

---

## 6. Cifrado

El cifrado se realiza en el servidor por medio de la clase:

`CifradoService`

Esta clase contiene dos métodos principales:

- `Cifrar(textoPlano)`
- `Descifrar(textoCifrado)`

El algoritmo utilizado en la implementación es **AES**.

---

## 7. Ejecución del proyecto

Para ejecutar el proyecto se deben seguir estos pasos:

1. Abrir el proyecto en **Visual Studio**.
2. Verificar que **SQL Server** esté activo.
3. Ejecutar el script SQL para crear la base de datos y las tablas.
4. Confirmar la cadena de conexión en `appsettings.json`.
5. Ejecutar el proyecto con **F5** o con el botón de inicio.
6. Abrir la dirección local mostrada por el sistema.

Ejemplo de dirección utilizada en el desarrollo:

`https://localhost:7002/`

---

## 8. Script de base de datos

El proyecto incluye un script SQL para crear la base de datos y las tablas necesarias.

Nombre sugerido del archivo:

`01_CrearBaseYTablas.sql`

Ubicación sugerida dentro del repositorio:

`ScriptsSQL/01_CrearBaseYTablas.sql`

---

## 9. Publicación del código fuente en GitHub

El código fuente fue subido a GitHub para facilitar la revisión del proyecto.

Repositorio:

`https://github.com/SteveAlvizures/SistemaCifradoToken`

---

## 10. Observaciones técnicas

- El proyecto fue desarrollado como aplicación web local.
- El cifrado y descifrado se realizan en el servidor.
- El historial registra intentos exitosos y fallidos.
- La eliminación aplicada en los mensajes es lógica.
- El proyecto sigue una estructura MVC para facilitar su organización y revisión.
