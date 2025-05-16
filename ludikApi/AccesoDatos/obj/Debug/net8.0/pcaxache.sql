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

EXEC sp_rename N'[PerfilesEstudiantes].[grupoId]', N'GrupoId', 'COLUMN';

EXEC sp_rename N'[PerfilesEstudiantes].[estudianteId]', N'EstudianteId', 'COLUMN';

EXEC sp_rename N'[Grupos].[profesorId]', N'ProfesorId', 'COLUMN';

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuarios]') AND [c].[name] = N'nombreUsuario_Nombre');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Usuarios] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [Usuarios] ALTER COLUMN [nombreUsuario_Nombre] nvarchar(20) NULL;

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuarios]') AND [c].[name] = N'NombreCompleto_Nombre');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Usuarios] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Usuarios] ALTER COLUMN [NombreCompleto_Nombre] nvarchar(20) NOT NULL;

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuarios]') AND [c].[name] = N'NombreCompleto_Apellido');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Usuarios] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Usuarios] ALTER COLUMN [NombreCompleto_Apellido] nvarchar(20) NOT NULL;

ALTER TABLE [Usuarios] ADD [correo_Correro] nvarchar(450) NULL;

ALTER TABLE [Tiendas] ADD [GrupoId] int NOT NULL DEFAULT 0;

ALTER TABLE [TablasEquivalencia] ADD [Nombre] nvarchar(50) NOT NULL DEFAULT N'';

ALTER TABLE [TablasClasificacion] ADD [Nombre] nvarchar(50) NOT NULL DEFAULT N'';

ALTER TABLE [Recompensas] ADD [Nombre] nvarchar(50) NOT NULL DEFAULT N'';

ALTER TABLE [Medallas] ADD [Nombre] nvarchar(50) NOT NULL DEFAULT N'';

ALTER TABLE [Grupos] ADD [nombre] nvarchar(30) NOT NULL DEFAULT N'';

CREATE UNIQUE INDEX [IX_Usuarios_correo_Correro] ON [Usuarios] ([correo_Correro]) WHERE [correo_Correro] IS NOT NULL;

CREATE UNIQUE INDEX [IX_Usuarios_nombreUsuario_Nombre] ON [Usuarios] ([nombreUsuario_Nombre]) WHERE [nombreUsuario_Nombre] IS NOT NULL;

CREATE INDEX [IX_TablasEquivalencia_Nombre] ON [TablasEquivalencia] ([Nombre]);

CREATE INDEX [IX_TablasClasificacion_Nombre] ON [TablasClasificacion] ([Nombre]);

CREATE INDEX [IX_Recompensas_Nombre] ON [Recompensas] ([Nombre]);

CREATE UNIQUE INDEX [UX_PerfilEstudiante_GrupoId_EstudianteId] ON [PerfilesEstudiantes] ([GrupoId], [EstudianteId]);

CREATE INDEX [IX_Medallas_Nombre] ON [Medallas] ([Nombre]);

CREATE INDEX [IX_Grupo_ProfesorId] ON [Grupos] ([ProfesorId]);

CREATE INDEX [IX_Grupos_nombre] ON [Grupos] ([nombre]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250513181824_SegundaConDataAnnotation', N'9.0.4');

DROP INDEX [IX_Usuarios_nombreUsuario_Nombre] ON [Usuarios];

EXEC sp_rename N'[Usuarios].[nombreUsuario_Nombre]', N'NombreUsuario_Nombre', 'COLUMN';

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Usuarios]') AND [c].[name] = N'NombreUsuario_Nombre');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Usuarios] DROP CONSTRAINT [' + @var3 + '];');
UPDATE [Usuarios] SET [NombreUsuario_Nombre] = N'' WHERE [NombreUsuario_Nombre] IS NULL;
ALTER TABLE [Usuarios] ALTER COLUMN [NombreUsuario_Nombre] nvarchar(20) NOT NULL;
ALTER TABLE [Usuarios] ADD DEFAULT N'' FOR [NombreUsuario_Nombre];

CREATE UNIQUE INDEX [IX_Usuarios_NombreUsuario_Nombre] ON [Usuarios] ([NombreUsuario_Nombre]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250514133044_ComprobarParaLogin', N'9.0.4');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250515135057_actualizacionConBaseAlPullQUeHice', N'9.0.4');

COMMIT;
GO

