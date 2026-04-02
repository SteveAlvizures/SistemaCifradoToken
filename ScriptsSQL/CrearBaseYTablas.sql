IF DB_ID('SistemaCifradoTokenDB') IS NULL
BEGIN
    CREATE DATABASE SistemaCifradoTokenDB;
END
GO

USE SistemaCifradoTokenDB;
GO

IF OBJECT_ID('dbo.Usuarios', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Usuarios (
        IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
        Nombre NVARCHAR(100) NOT NULL,
        Correo NVARCHAR(150) NOT NULL UNIQUE,
        NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(255) NOT NULL,
        Estado NVARCHAR(20) NOT NULL DEFAULT 'Activo',
        FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

IF OBJECT_ID('dbo.Mensajes', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Mensajes (
        IdMensaje INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuarioPropietario INT NOT NULL,
        TextoCifrado NVARCHAR(MAX) NOT NULL,
        HashIntegridad NVARCHAR(255) NULL,
        Token NVARCHAR(100) NOT NULL UNIQUE,
        Etiqueta NVARCHAR(100) NULL,
        Estado NVARCHAR(20) NOT NULL DEFAULT 'Activo',
        FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
        FechaPrimerUso DATETIME NULL,
        FechaEliminacion DATETIME NULL,
        TotalIntentos INT NOT NULL DEFAULT 0,
        TotalExitosos INT NOT NULL DEFAULT 0,
        CONSTRAINT FK_Mensajes_Usuarios
            FOREIGN KEY (IdUsuarioPropietario) REFERENCES dbo.Usuarios(IdUsuario)
    );
END
GO

IF OBJECT_ID('dbo.HistorialAccesos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.HistorialAccesos (
        IdHistorial INT IDENTITY(1,1) PRIMARY KEY,
        IdMensaje INT NULL,
        IdUsuarioAccion INT NOT NULL,
        TokenIngresado NVARCHAR(100) NOT NULL,
        Resultado NVARCHAR(30) NOT NULL,
        Motivo NVARCHAR(255) NULL,
        DireccionIP NVARCHAR(50) NULL,
        UserAgent NVARCHAR(300) NULL,
        Dispositivo NVARCHAR(100) NULL,
        FechaHora DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_Historial_Mensajes
            FOREIGN KEY (IdMensaje) REFERENCES dbo.Mensajes(IdMensaje),
        CONSTRAINT FK_Historial_Usuarios
            FOREIGN KEY (IdUsuarioAccion) REFERENCES dbo.Usuarios(IdUsuario)
    );
END
GO
