# AGENTS.md — LudikAPI

## 1. Propósito, alcance y precedencia

Este archivo gobierna el trabajo de agentes dentro de `LudikAPI/`. Un `AGENTS.md` ubicado en una carpeta descendiente puede añadir o reemplazar reglas para ese subárbol.

El objetivo es producir cambios pequeños, seguros, trazables y verificables sin cargar documentación irrelevante. Aplica este orden de precedencia:

1. Instrucciones explícitas de la persona usuaria.
2. El `AGENTS.md` más cercano al archivo modificado.
3. Las fuentes de verdad en `../docs/` enrutadas por la sección 3.
4. Convenciones observadas en el código y pruebas del módulo afectado.
5. Este documento.

No uses el orden anterior para ignorar una contradicción silenciosamente. Si una petición, un requerimiento, la arquitectura documentada y el código vigente no coinciden, detén la implementación que dependa de esa decisión, presenta la evidencia concreta y solicita una resolución. No inventes reglas de negocio.

## 2. Contexto mínimo siempre activo

- Plataforma: .NET 8 y ASP.NET Core 8.
- Persistencia: EF Core 8 sobre SQL Server.
- Autenticación y autorización: ASP.NET Core Identity y JWT.
- Procesos en segundo plano: Hangfire.
- Archivos: Azure Blob Storage; en desarrollo puede utilizarse Azurite.
- Documentación HTTP: Swagger/OpenAPI y comentarios XML.
- Pruebas: xUnit, Moq y `WebApplicationFactory<Program>`.

Capas y dirección permitida de dependencias:

```text
WebApi ──► LogicaAplicacion ──► LogicaNegocio
   │                                ▲
   └────────► AccesoDatos ──────────┘
```

- `LogicaNegocio/`: entidades, value objects, reglas de dominio, resultados e interfaces de repositorios.
- `LogicaAplicacion/`: casos de uso, DTOs, mappers, eventos y contratos de servicios.
- `AccesoDatos/`: `DbContext`, configuraciones de EF Core, migraciones, repositorios y adaptadores de infraestructura.
- `WebApi/`: controladores, autenticación, composición de dependencias, Swagger y servicios propios del host.
- `PruebasUnitarias/`: pruebas aisladas de dominio y casos de uso.
- `PruebasIntegracion/`: pruebas del host HTTP y sus integraciones.

Mantén los controladores delgados, las reglas de negocio en el dominio, la coordinación en aplicación y la implementación de persistencia en acceso a datos. No uses `ContextoDb` directamente desde controladores o casos de uso.

## 3. Protocolo de enrutamiento de contexto

### 3.1 Regla de carga progresiva

No leas toda la carpeta `../docs/` al comenzar una tarea. Primero inspecciona únicamente:

1. La petición y el estado de Git.
2. Los archivos que probablemente cambiarán.
3. Sus pruebas, contratos y llamadas directas.
4. Sólo las fuentes documentales activadas por la matriz siguiente.

Busca por identificador, entidad o interfaz y abre el fragmento pertinente antes de ampliar el contexto. Ejemplos:

```powershell
rg -n -C 12 '^### \[RF12\]' ..\docs\requerimientos\01_requerimientos_funcionales.md
rg -n -C 8 'TablaEquivalencia|Equivalencia' ..\docs\arquitectura\02_Modelo_LogicaNegocio.md
rg -n -C 8 'IRepositorioGrupos' ..\docs\arquitectura\01_Interfaces_AccesoDatos.md
```

Amplía la lectura sólo cuando el fragmento no alcance para entender dependencias, reglas relacionadas o límites del cambio. No cargues documentos de diagramas `.asta`: no son una fuente textual operativa para el agente.

### 3.2 Matriz de decisión

| Disparador observado | Contexto obligatorio | Contexto que no se carga por defecto |
| --- | --- | --- |
| La petición menciona `RF<n>`, modifica un caso de uso, endpoint o regla funcional | Sección del RF indicado en `../docs/requerimientos/01_requerimientos_funcionales.md`, sus RF declarados como dependencias y pruebas/código del flujo | RF de otros módulos |
| El cambio afecta rendimiento, concurrencia, idioma, experiencia móvil, diseño responsivo o compatibilidad | RNF aplicables de `../docs/requerimientos/02_requerimientos_no_funcionales.md` | Requerimientos funcionales no relacionados |
| Se crea o modifica una entidad, value object, relación o invariante de dominio | Entidades y relaciones afectadas en `../docs/arquitectura/02_Modelo_LogicaNegocio.md`; RF/RNF que motivan el cambio | Modelo completo si las relaciones cercanas bastan |
| Se crea o modifica un repositorio, consulta, unidad de trabajo o persistencia | Interfaces afectadas en `../docs/arquitectura/01_Interfaces_AccesoDatos.md`, modelo de dominio relacionado y configuración EF/repo vigente | Interfaces no afectadas |
| Se cambia un contrato HTTP, autenticación, autorización, claims, Identity o JWT | RF/RNF aplicables, controlador, DTO, caso de uso, configuración y pruebas de integración relacionadas | Arquitectura no relacionada |
| Se cambia el esquema persistente o una relación EF | Ambos documentos de arquitectura, RF/RNF aplicables, configuraciones EF y última migración relacionada | Requerimientos de otros módulos |
| Sólo cambia documentación, formato o configuración sin comportamiento | Documento/configuración afectado y referencias directas | Requerimientos y arquitectura completos |
| La tarea implementa o modifica una funcionalidad y está lista para validarse | La lista determinista indicada en la sección 6 | La lista humana, salvo petición explícita |

Si no se conoce el RF aplicable, búscalo por vocabulario de dominio con `rg`; no leas secuencialmente el catálogo completo. Registra los identificadores RF/RNF aplicables en las pruebas, documentación o resumen final cuando eso mejore la trazabilidad.

### 3.3 Autoridad y vigencia documental

- Los requerimientos describen el comportamiento esperado; el código existente no invalida por sí solo una regla documentada.
- Los documentos de arquitectura describen el diseño objetivo, pero debes contrastarlos con las firmas y relaciones actuales antes de editar. Una diferencia puede ser deuda o documentación desactualizada, no autorización para una migración amplia.
- No corrijas documentos, código o consumidores fuera del alcance sólo para forzar consistencia. Informa la divergencia y limita el cambio.
- Si una ruta enrutada no existe o no puede leerse, declara el bloqueo; no sustituyas la fuente por memoria o suposiciones.

## 4. Flujo de implementación

1. Clasifica la tarea: documentación/configuración, corrección, RF nuevo o modificado, contrato HTTP, dominio, persistencia, seguridad o migración.
2. Inspecciona `git status --short` y `git diff` antes de editar. Los cambios existentes pertenecen a la persona usuaria: presérvalos y no los reformatees.
3. Aplica la matriz de contexto y anota para ti los RF/RNF, entidades, contratos y riesgos activos.
4. Define resultados observables y pruebas proporcionales al riesgo. Para un bug, crea primero una prueba de regresión y confirma que falla por la causa esperada.
5. Implementa el cambio mínimo que resuelva la causa. Evita dependencias, abstracciones y refactorizaciones no exigidas.
6. Ejecuta las puertas de validación de la sección 6. Una validación no ejecutada nunca equivale a una validación aprobada.
7. Revisa el diff final y reporta archivos cambiados, comandos ejecutados, resultados y cualquier riesgo o comprobación pendiente.

## 5. Reglas de implementación

### Código y contratos

- Sigue el estilo del módulo cercano. Conserva el vocabulario español del dominio y evita renombrados cosméticos.
- Mantén `Nullable` habilitado. No ocultes advertencias con `!` salvo que la invariancia esté demostrada claramente en el código.
- Usa `async`/`await` para I/O y el sufijo `Async` en métodos asíncronos nuevos. No bloquees tareas con `.Result`, `.Wait()` o `.GetAwaiter().GetResult()`.
- Prefiere inyección por constructor y registra implementaciones nuevas con la vida útil correcta en el punto de composición de `WebApi/Program.cs`.
- Modela fallos esperables mediante `Resultado`/`Resultado<T>` y `Error`; reserva excepciones para fallos excepcionales o de infraestructura.
- Valida entradas en el límite apropiado y preserva invariantes en el dominio. No dupliques una misma regla en controlador, caso de uso y repositorio.
- Trata rutas, DTOs, códigos HTTP, JSON y discriminadores como contratos públicos. Si cambia un endpoint, actualiza sus comentarios XML, metadatos Swagger, consumidores y pruebas correspondientes.
- Evita cambios incompatibles de contrato. Si son inevitables y están autorizados, documenta explícitamente la ruptura y su estrategia de migración.
- Usa el manejo de errores compartido de `WebApi/Helpers`. No expongas trazas, secretos ni detalles internos.
- Conserva el formato de los archivos existentes; no apliques formato global a código legado durante un cambio acotado.
- No edites artefactos generados, `bin/`, `obj/`, resultados de pruebas o archivos de publicación.

### Seguridad

- En endpoints autenticados, obtiene la identidad desde claims del servidor y verifica propiedad, pertenencia y política. No confíes en identificadores ni roles enviados por el cliente.
- Nunca agregues contraseñas, tokens, claves, cadenas de conexión reales, perfiles de publicación ni datos personales al repositorio.
- Usa Secret Manager en desarrollo y variables de entorno o un almacén de secretos en despliegues. En variables de entorno, separa secciones con `__`.
- Conserva en `appsettings*.json` únicamente valores no sensibles o placeholders seguros.
- Usa logging estructurado con la mínima información necesaria; no registres credenciales, tokens ni respuestas de seguridad.
- Revisa explícitamente autenticación, autorización y exposición de datos en cambios de JWT, Identity, CORS, Hangfire Dashboard, archivos o almacenamiento.
- Para archivos, limita tamaño y dimensiones, valida el contenido real y asigna nombres controlados por el servidor.

### Persistencia y migraciones

- Mantén lecturas y escrituras en `AccesoDatos`; usa proyecciones o consultas acotadas en lugar de cargar grafos innecesarios.
- Respeta el límite transaccional y el `IUnitOfWork` existente para cambios relacionados.
- Todo cambio del modelo persistente debe incluir una migración EF y el snapshot generados por herramientas; no los escribas manualmente.
- Revisa el código generado y usa un nombre de migración descriptivo.
- No ejecutes migraciones en bases compartidas, remotas o de producción sin autorización explícita.

```powershell
dotnet ef migrations add <NombreDescriptivo> --project .\AccesoDatos\AccesoDatos.csproj --startup-project .\WebApi\WebApi.csproj
dotnet ef database update --project .\AccesoDatos\AccesoDatos.csproj --startup-project .\WebApi\WebApi.csproj
```

## 6. Validación determinista

### 6.1 Fuente ejecutable de Definition of Done

Para toda corrección o cambio de comportamiento, abre al final —no al inicio— `../docs/lista_verificacion_agente_ia_desarrollo_requerimiento_funciona.md` y trátala como una lista de puertas obligatorias. Esa es la lista del agente; `../docs/lista_verificacion_humana_desarrollo_requerimiento_funcional.md` no la reemplaza y sólo se consulta si la persona usuaria lo pide.

No marques una condición por intuición. Cada puerta debe quedar en uno de estos estados:

- `PASS`: existe evidencia reproducible (comando con código de salida cero, prueba o diff verificable).
- `FAIL`: el comando o comprobación produjo evidencia contraria; corrige antes de terminar.
- `BLOCKED`: no puede ejecutarse por una dependencia externa o falta de autorización; informa el comando, el error y el impacto.
- `N/A`: el disparador objetivo no ocurrió; indica brevemente por qué.

No conviertas `FAIL` en `PASS` debilitando pruebas, suprimiendo advertencias o excluyendo archivos. No declares éxito global con alguna puerta obligatoria en `FAIL`. Un `BLOCKED` tampoco cuenta como aprobado.

### 6.2 Puertas y comandos

Ejecuta desde `LudikAPI/`, en este orden, para cambios de código:

```powershell
dotnet restore .\LudikAPI.sln
dotnet build .\LudikAPI.sln --configuration Release --no-restore
dotnet test .\PruebasUnitarias\PruebasUnitarias.csproj --configuration Release --no-build
```

El código de salida debe ser `0`; el build debe informar `0 Warning(s)` y `0 Error(s)`. El mero hecho de que el agente considere correcto el código no sustituye estos resultados.

Ejecuta además la suite de integración cuando cambien rutas, serialización, autenticación/autorización, DI, persistencia, migraciones, almacenamiento o comportamiento del host:

```powershell
dotnet test .\PruebasIntegracion\PruebasIntegracion.csproj --configuration Release --no-build
```

Estas pruebas pueden requerir SQL Server, configuración válida, datos sembrados y almacenamiento. Si falta infraestructura, clasifica la puerta como `BLOCKED` y conserva el error exacto; no la declares aprobada.

Durante la iteración puedes ejecutar una prueba acotada, pero no reemplaza las puertas finales:

```powershell
dotnet test .\PruebasUnitarias\PruebasUnitarias.csproj --filter "FullyQualifiedName~NombreDeLaPrueba"
```

Comprueba objetivamente el alcance y los artefactos antes de finalizar:

```powershell
git status --short
git diff --check
git diff --name-only
git diff --stat
```

Revisa la lista de nombres devuelta: ningún cambio nuevo puede pertenecer a `bin/`, `obj/`, `TestResults/` o artefactos generados, salvo instrucción explícita aplicable. Usa `git diff --name-only --diff-filter=ACMRTUXB` para separar cambios actuales de eliminaciones preexistentes.

Para cambios exclusivamente documentales, no ejecutes restore/build/test salvo que el documento altere código ejecutable, generación, configuración de compilación o comandos automatizados. Las puertas mínimas son `git diff --check`, inspección del diff y verificación de que todas las rutas y comandos documentados existen o son sintácticamente coherentes.

### 6.3 Evidencia por tipo de cambio

- Corrección: prueba de regresión observada en rojo antes y verde después, más las puertas base.
- Dominio o caso de uso: pruebas positivas, negativas y de cambios de estado observables.
- Endpoint o seguridad: prueba de integración de éxito, no autenticado, no autorizado y entrada inválida según aplique.
- Persistencia: pruebas de consulta/escritura, revisión de migración y snapshot, y suite de integración.
- Contrato público: diff de XML/Swagger/DTO y prueba de serialización o HTTP.
- Documentación solamente: enlaces/rutas verificadas y diff limpio; código clasificado como `N/A`.

## 7. Formato de entrega

El resumen final debe incluir:

- Qué cambió y qué RF/RNF guiaron la implementación, cuando aplique.
- Validaciones ejecutadas con su resultado (`PASS`, `BLOCKED` o `N/A`; no ocultes `FAIL`).
- Pruebas o validaciones no ejecutadas y motivo concreto.
- Riesgos, divergencias documentales o decisiones pendientes.

No afirmes que “todo está correcto” sin la evidencia exigida por la sección 6.
