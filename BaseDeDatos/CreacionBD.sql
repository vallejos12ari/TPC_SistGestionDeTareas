CREATE DATABASE GESTION_TAREAS_DB;
GO

USE GESTION_TAREAS_DB;
GO

-- TABLAS

CREATE TABLE ESTADO (
    IdEstado INT PRIMARY KEY IDENTITY(1,1),
    NombreEstado NVARCHAR(50) NOT NULL UNIQUE
	Color NVARCHAR(10) NULL
);


CREATE TABLE PRIORIDAD (
    IdPrioridad INT PRIMARY KEY IDENTITY(1,1),
    NombrePrioridad NVARCHAR(50) NOT NULL UNIQUE
);


--INSERT INTO ESTADO (Nombre) VALUES
--('Por hacer'),    -- ID 1
--('En progreso'),   -- ID 2
--('Completada'),     -- ID 3
--('Cancelada');     -- ID 4
--GO

--INSERT INTO PRIORIDAD (Nombre) VALUES
--('Baja'),    -- ID 1
--('Media'),   -- ID 2
--('Alta');     -- ID 3
--GO

CREATE TABLE TAG (
    IdTag INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL UNIQUE
);


CREATE TABLE USUARIO (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario NVARCHAR(100) NOT NULL UNIQUE,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    ContraseniaHash NVARCHAR(256) NOT NULL,
    Rol NVARCHAR(20) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1, -- 1 = activo, 0 = baja lgica
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaActualizacion DATETIME NULL
);


-- TOKEN

CREATE TABLE AutenticacionToken (
    IdToken INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL,
    Token NVARCHAR(500) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaExpiracion DATETIME NOT NULL,
    
    CONSTRAINT FK_AutenticacionToken_Usuario FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario)
);


-- TAREA

CREATE TABLE TAREA (
    IdTarea INT PRIMARY KEY IDENTITY(1,1),
    IdEstado INT NOT NULL,
    IdPrioridad INT NOT NULL,
    IdUsuarioCreador INT NOT NULL,
    IdUsuarioAsignado INT NULL, -- Es NULL si la tarea no est asignada a nadie
    Titulo NVARCHAR(150) NOT NULL,
    Descripcion NVARCHAR(MAX) NULL,
    Activo BIT NOT NULL DEFAULT 1, -- 1 = activa, 0 = baja lgica
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaVencimiento DATETIME NULL,
    FechaActualizacion DATETIME NULL,


    CONSTRAINT FK_Tarea_Estado FOREIGN KEY (IdEstado) REFERENCES ESTADO(IdEstado),
    CONSTRAINT FK_Tarea_Prioridad FOREIGN KEY (IdPrioridad) REFERENCES PRIORIDAD(IdPrioridad),
    

    CONSTRAINT FK_Tarea_UsuarioCreador FOREIGN KEY (IdUsuarioCreador) REFERENCES USUARIO(IdUsuario),
    CONSTRAINT FK_Tarea_UsuarioAsignado FOREIGN KEY (IdUsuarioAsignado) REFERENCES USUARIO(IdUsuario)
);


CREATE TABLE Imagen (
    IdImagen INT PRIMARY KEY IDENTITY(1,1),
    IdTarea INT NOT NULL,
    UrlImagen NVARCHAR(255) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),


    CONSTRAINT FK_Imagen_Tarea FOREIGN KEY (IdTarea) REFERENCES TAREA(IdTarea)
);



CREATE TABLE TareasTags (
    IdTarea INT NOT NULL,
    IdTag INT NOT NULL,

    -- PK Compuesta
    CONSTRAINT PK_TareasTags PRIMARY KEY (IdTarea, IdTag),

    -- FKs a TAREA y TAG
    CONSTRAINT FK_TareasTags_Tarea FOREIGN KEY (IdTarea) REFERENCES TAREA(IdTarea),
    CONSTRAINT FK_TareasTags_Tag FOREIGN KEY (IdTag) REFERENCES TAG(IdTag)
);

-- ORDEN_ESTADOS
CREATE TABLE ORDEN_ESTADOS (
    IdOrden INT PRIMARY KEY IDENTITY(1,1),
    IdEstadoActual INT NOT NULL,
    IdEstadoDestino INT NOT NULL,
    CONSTRAINT FK_Orden_EstadoActual FOREIGN KEY (IdEstadoActual) REFERENCES ESTADO(IdEstado),
    CONSTRAINT FK_Orden_EstadoDestino FOREIGN KEY (IdEstadoDestino) REFERENCES ESTADO(IdEstado)
);

     CREATE TABLE USUARIO_RELACION (
     IdUsuario INT NOT NULL,
     IdUsuarioRelacionado INT NOT NULL,
     CONSTRAINT PK_UsuarioRelacion PRIMARY KEY (IdUsuario, IdUsuarioRelacionado),
     CONSTRAINT FK_UsuarioRelacion_Usuario FOREIGN KEY (IdUsuario) REFERENCES USUARIO(IdUsuario),
     CONSTRAINT FK_UsuarioRelacion_UsuarioRelacionado FOREIGN KEY (IdUsuarioRelacionado) REFERENCES
     USUARIO(IdUsuario)
);