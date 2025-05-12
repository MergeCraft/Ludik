IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [BarrasProgreso] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK_BarrasProgreso] PRIMARY KEY ([Id])
);

CREATE TABLE [EnlacesUnion] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK_EnlacesUnion] PRIMARY KEY ([Id])
);

CREATE TABLE [Equivalencias] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK_Equivalencias] PRIMARY KEY ([Id])
);

CREATE TABLE [Grupos] (
    [Id] int NOT NULL IDENTITY,
    [profesorId] int NOT NULL,
    CONSTRAINT [PK_Grupos] PRIMARY KEY ([Id])
);

CREATE TABLE [Hitos] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK_Hitos] PRIMARY KEY ([Id])
);

CREATE TABLE [Medallas] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK_Medallas] PRIMARY KEY ([Id])
);

CREATE TABLE [PerfilesEstudiantes] (
    [Id] int NOT NULL IDENTITY,
    [estudianteId] int NOT NULL,
    [grupoId] int NOT NULL,
    CONSTRAINT [PK_PerfilesEstudiantes] PRIMARY KEY ([Id])
);

CREATE TABLE [Pines] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK_Pines] PRIMARY KEY ([Id])
);

CREATE TABLE [PreguntasRespuestasSeguridad] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK_PreguntasRespuestasSeguridad] PRIMARY KEY ([Id])
);

CREATE TABLE [Recompensas] (
    [Id] int NOT NULL IDENTITY,
    [Discriminator] nvarchar(13) NOT NULL,
    CONSTRAINT [PK_Recompensas] PRIMARY KEY ([Id])
);

CREATE TABLE [RendimientosPeriodos] (
    [Id] int NOT NULL IDENTITY,
    [perfilEstudianteId] int NOT NULL,
    CONSTRAINT [PK_RendimientosPeriodos] PRIMARY KEY ([Id])
);

CREATE TABLE [SolicitudesUnion] (
    [Id] int NOT NULL IDENTITY,
    [grupoId] int NOT NULL,
    CONSTRAINT [PK_SolicitudesUnion] PRIMARY KEY ([Id])
);

CREATE TABLE [TablasClasificacion] (
    [Id] int NOT NULL IDENTITY,
    [grupoId] int NOT NULL,
    CONSTRAINT [PK_TablasClasificacion] PRIMARY KEY ([Id])
);

CREATE TABLE [TablasEquivalencia] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK_TablasEquivalencia] PRIMARY KEY ([Id])
);

CREATE TABLE [Tiendas] (
    [Id] int NOT NULL IDENTITY,
    CONSTRAINT [PK_Tiendas] PRIMARY KEY ([Id])
);

CREATE TABLE [Usuarios] (
    [Id] int NOT NULL IDENTITY,
    [contrasenia_Clave] nvarchar(max) NOT NULL,
    [Discriminator] nvarchar(13) NOT NULL,
    [NombreCompleto_Apellido] nvarchar(max) NOT NULL,
    [NombreCompleto_Nombre] nvarchar(max) NOT NULL,
    [nombreUsuario_Nombre] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250512174516_primeraMigracion', N'9.0.4');

COMMIT;
GO

