# AGENTS.md — LudikAPI

## Propósito y alcance

Este archivo define cómo deben trabajar los agentes de desarrollo dentro de `LudikAPI/`. Sus instrucciones se aplican a todo este subárbol. Si existe un `AGENTS.md` más específico en una carpeta descendiente, ese archivo complementa o reemplaza estas reglas para su propio alcance.

El objetivo es entregar cambios pequeños, seguros, verificables y coherentes con la arquitectura existente. Las instrucciones explícitas de la persona usuaria tienen prioridad sobre este documento.

## Contexto técnico

- Plataforma: .NET 8 y ASP.NET Core 8.
- Persistencia: EF Core 8 sobre SQL Server.
- Autenticación y autorización: ASP.NET Core Identity y JWT.
- Procesos en segundo plano: Hangfire.
- Archivos: Azure Blob Storage; en desarrollo puede utilizarse Azurite.
- Documentación HTTP: Swagger/OpenAPI y comentarios XML.
- Pruebas: xUnit, Moq y `WebApplicationFactory<Program>`.

La solución está organizada en estas capas:

- `LogicaNegocio/`: entidades, value objects, reglas de dominio, resultados e interfaces de repositorios.
- `LogicaAplicacion/`: casos de uso, DTOs, mappers, eventos y contratos de servicios.
- `AccesoDatos/`: `DbContext`, configuraciones de EF Core, migraciones, repositorios y adaptadores de infraestructura.
- `WebApi/`: controladores, autenticación, composición de dependencias, Swagger y servicios propios del host.
- `PruebasUnitarias/`: pruebas aisladas de dominio y casos de uso.
- `PruebasIntegracion/`: pruebas del host HTTP y sus integraciones.

## Principios de trabajo

1. Antes de modificar código, lee los archivos relacionados, sus pruebas y el estado de Git. No sobrescribas cambios ajenos ni reformatees archivos no relacionados.
2. Resuelve la causa del problema con el cambio mínimo necesario. Evita refactorizaciones amplias, nuevas abstracciones o dependencias sin una necesidad concreta.
3. Conserva la dirección de dependencias: dominio no depende de aplicación, infraestructura ni Web API; aplicación depende del dominio; infraestructura implementa contratos; Web API actúa como punto de composición y transporte HTTP.
4. Mantén los controladores delgados. Coloca reglas de negocio en `LogicaNegocio` y coordinación de flujos en `LogicaAplicacion`.
5. Usa los contratos de repositorio y servicios existentes. No accedas directamente a `ContextoDb` desde controladores o casos de uso.
6. Trata rutas, DTOs, códigos HTTP, cuerpos JSON y discriminadores como contratos públicos. Evita cambios incompatibles; si son inevitables, documéntalos y actualiza consumidores y pruebas.
7. No edites artefactos generados ni carpetas `bin/`, `obj/`, resultados de pruebas o archivos de publicación.

## Convenciones de implementación

- Sigue el estilo del archivo y del módulo cercanos. El dominio utiliza vocabulario en español; conserva ese lenguaje en nombres públicos y evita renombrados cosméticos.
- Mantén `Nullable` habilitado. No ocultes advertencias mediante `!` salvo que la invariancia esté demostrada y sea clara en el código.
- Usa `async`/`await` para I/O y el sufijo `Async` en métodos asíncronos nuevos. No bloquees tareas con `.Result`, `.Wait()` o `.GetAwaiter().GetResult()`.
- Prefiere inyección por constructor y registra cada implementación nueva con la vida útil adecuada en el punto de composición de `WebApi/Program.cs`.
- Modela fallos esperables mediante `Resultado`/`Resultado<T>` y `Error`, respetando el patrón del módulo. Reserva excepciones para situaciones verdaderamente excepcionales.
- Valida entradas en el límite apropiado y preserva invariantes dentro del dominio. No dupliques la misma regla en controlador, caso de uso y repositorio.
- En endpoints autenticados, obtiene la identidad desde los claims y verifica propiedad, pertenencia y política de autorización en el servidor. No confíes en identificadores o roles enviados por el cliente.
- Devuelve códigos HTTP consistentes y utiliza el manejo de errores compartido de `WebApi/Helpers`. No expongas trazas, secretos ni mensajes internos de excepciones en las respuestas.
- Añade o actualiza comentarios XML y metadatos de Swagger cuando cambie el comportamiento público de un endpoint.
- Conserva el formato de los archivos existentes. No apliques formato global a código legado como parte de un cambio acotado.

## Persistencia y migraciones

- Mantén consultas y escritura de datos en `AccesoDatos`; evita cargar grafos completos cuando una proyección o consulta acotada sea suficiente.
- Para operaciones con varios cambios relacionados, respeta el límite transaccional y el `IUnitOfWork` existente.
- Los cambios de modelo persistente deben incluir una migración de EF Core y la actualización del snapshot. Genera ambos con herramientas de EF; no los escribas manualmente.
- Usa nombres descriptivos para las migraciones y revisa el código generado antes de entregarlo.
- No ejecutes migraciones contra bases compartidas, remotas o de producción sin autorización explícita.

Comandos de referencia, ejecutados desde `LudikAPI/`:

```powershell
dotnet ef migrations add <NombreDescriptivo> --project .\AccesoDatos\AccesoDatos.csproj --startup-project .\WebApi\WebApi.csproj
dotnet ef database update --project .\AccesoDatos\AccesoDatos.csproj --startup-project .\WebApi\WebApi.csproj
```

## Configuración y seguridad

- Nunca agregues al repositorio contraseñas, tokens JWT, claves de Azure, cadenas de conexión reales, perfiles de publicación ni datos personales.
- Usa Secret Manager de .NET para desarrollo local y variables de entorno o un almacén de secretos en entornos desplegados.
- Conserva en `appsettings*.json` únicamente valores no sensibles o placeholders seguros. Recuerda que las variables de entorno representan secciones con `__`, por ejemplo `ConnectionStrings__DefaultConnection`.
- No registres tokens, credenciales, respuestas de seguridad ni datos personales. Usa logging estructurado y la mínima información necesaria.
- Cualquier cambio en JWT, Identity, CORS, Hangfire Dashboard, carga de archivos o almacenamiento requiere una revisión explícita de autenticación, autorización y exposición de datos.
- Al procesar archivos, limita tamaño y dimensiones, valida el contenido real y genera nombres controlados por el servidor.

## Pruebas y validación

Cada cambio de comportamiento debe incluir pruebas proporcionales al riesgo. Prioriza una prueba de regresión que falle antes del arreglo y pase después.

- Prueba reglas de dominio y casos de uso en `PruebasUnitarias/` con xUnit y Moq. Verifica resultados observables, errores y efectos; evita afirmar detalles internos sin valor contractual.
- Usa `PruebasIntegracion/` para rutas, serialización, autenticación, DI, persistencia y comportamiento del host.
- Las pruebas de integración actuales no son herméticas: pueden requerir SQL Server, configuración válida, datos sembrados y servicios de almacenamiento. No las declares aprobadas si la infraestructura necesaria no estuvo disponible; informa la causa exacta.
- Corrige el código de producción cuando una prueba válida falla. No debilites aserciones ni omitas pruebas para obtener un resultado verde.

Validación base desde `LudikAPI/`:

```powershell
dotnet restore .\LudikAPI.sln
dotnet build .\LudikAPI.sln --configuration Release --no-restore
dotnet test .\PruebasUnitarias\PruebasUnitarias.csproj --configuration Release --no-build
```

Cuando el entorno de integración esté preparado:

```powershell
dotnet test .\PruebasIntegracion\PruebasIntegracion.csproj --configuration Release --no-build
```

Durante la iteración puede ejecutarse una prueba acotada:

```powershell
dotnet test .\PruebasUnitarias\PruebasUnitarias.csproj --filter "FullyQualifiedName~NombreDeLaPrueba"
```

## Criterios de finalización

Antes de dar una tarea por terminada:

1. Revisa el diff completo y confirma que sólo contiene cambios relacionados.
2. Compila la solución y ejecuta las pruebas relevantes; amplía a la suite unitaria completa cuando cambie comportamiento compartido.
3. Verifica autorización, manejo de errores, compatibilidad del contrato HTTP y efectos sobre datos cuando apliquen.
4. Actualiza pruebas, documentación, configuración de ejemplo y migraciones que formen parte del cambio.
5. Informa de forma breve qué cambió, qué comandos se ejecutaron y sus resultados. Declara cualquier validación omitida, limitación o riesgo pendiente.

