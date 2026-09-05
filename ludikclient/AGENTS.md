# AGENTS.md — ludikclient

## Propósito y alcance

Este archivo define cómo deben trabajar los agentes de desarrollo dentro de `ludikclient/`. Sus instrucciones se aplican a todo este subárbol. Si existe un `AGENTS.md` más específico en una carpeta descendiente, ese archivo complementa o reemplaza estas reglas para su propio alcance.

El objetivo es entregar cambios pequeños, accesibles, seguros y verificables, manteniendo coherencia con el producto y con los contratos de LudikAPI. Las instrucciones explícitas de la persona usuaria tienen prioridad sobre este documento.

## Contexto técnico

- Runtime: Node.js 20 o superior y npm 10 o superior.
- UI: React 19 y Create React App mediante `react-scripts` 5.
- Navegación: React Router 7.
- Estado remoto: TanStack React Query 5.
- Estado global del cliente: Redux Toolkit, React Redux y `redux-persist`.
- HTTP: instancia compartida de Axios con interceptor JWT.
- Estilos: CSS Modules para estilos locales y `App.css` para primitivas globales deliberadas.
- Pruebas: Jest y React Testing Library a través de Create React App.

La aplicación está organizada de esta forma:

- `src/features/`: pantallas y componentes agrupados por capacidad de negocio.
- `src/features/*/hooks/`: hooks de consultas, mutaciones y estado de la feature.
- `src/services/`: adaptación del contrato HTTP de LudikAPI.
- `src/lib/`: infraestructura compartida, como Axios, notificaciones y normalización de errores.
- `src/app/`: configuración transversal de URL y store.
- `src/assets/` y `public/assets/`: recursos visuales estáticos.
- `public/`: documento HTML, fuentes y configuración estática del hosting.

## Principios de trabajo

1. Antes de modificar código, lee el componente, sus estilos, servicios, hooks, rutas y pruebas relacionados, además del estado de Git. No sobrescribas cambios ajenos.
2. Resuelve la necesidad con el cambio mínimo coherente. Evita refactorizaciones globales, actualizaciones de dependencias o reemplazos de librerías sin una justificación concreta.
3. Conserva los límites entre UI, estado y transporte: los componentes presentan e interactúan; los hooks coordinan datos; los servicios conocen HTTP; el backend aplica las reglas y la autorización definitivas.
4. Reutiliza componentes, hooks, servicios, estilos y utilidades existentes antes de crear variantes nuevas.
5. Trata rutas, parámetros, nombres de campos, formatos de error y respuestas de LudikAPI como contratos. Coordina cualquier cambio incompatible con el backend.
6. No edites `node_modules/`, `build/`, archivos generados ni `package-lock.json` manualmente. El lockfile sólo debe cambiar mediante npm y junto con un cambio deliberado de dependencias.

## Componentes y React

- Usa componentes funcionales y hooks. Mantén cada componente enfocado; extrae una pieza cuando tenga responsabilidad propia o reutilización real.
- Cumple las Rules of Hooks. No llames hooks dentro de condiciones, ciclos, callbacks arbitrarios ni funciones que no sean componentes o hooks personalizados.
- Calcula valores derivados durante el render cuando sea posible. Reserva `useEffect` para sincronizar con sistemas externos y declara todas sus dependencias reales.
- No modifiques directamente props, resultados de consultas ni estado. Usa actualizaciones inmutables y actualizadores funcionales cuando el valor nuevo dependa del anterior.
- Usa claves estables provenientes de los datos en listas. No uses el índice salvo que los elementos sean estáticos y nunca cambien de orden.
- Conserva el vocabulario del producto en español y la convención de nombres del módulo. Los componentes usan `PascalCase`; hooks y funciones, `camelCase`; los hooks comienzan con `use`.
- Declara `PropTypes` para componentes reutilizables nuevos o modificados que reciban props. Mantén defaults explícitos cuando sean relevantes.
- Elimina imports, estado, efectos, comentarios y código muerto que el cambio deje obsoletos, sin limpiar áreas no relacionadas.

## Estado y obtención de datos

Elige el nivel de estado más pequeño que resuelva la necesidad:

- Datos del servidor: React Query.
- Estado global duradero del cliente: Redux Toolkit; actualmente autenticación y preferencias visuales.
- Estado efímero de una pantalla o formulario: estado local del componente.
- Estado compartido por una rama pequeña: elévalo al ancestro común antes de agregarlo al store global.

Para React Query:

- Usa claves como arreglos e incluye todos los parámetros que cambian el resultado, por ejemplo `['grupo', id]`.
- Usa la API de TanStack Query 5 en código nuevo o modificado, incluidos objetos de filtros como `invalidateQueries({ queryKey: ['grupos'] })` y `gcTime` en lugar de `cacheTime`.
- Define `enabled` cuando una consulta dependa de identificadores, sesión u otros datos todavía no disponibles.
- Después de una mutación, invalida o actualiza únicamente las claves afectadas. Evita invalidaciones globales accidentales.
- Centraliza mensajes y visualización de errores mediante `src/lib/apiUtils.js` y `src/lib/toastify.js`.
- No copies datos remotos al Redux store ni a estado local sin una razón que requiera una edición independiente o un snapshot.

## Servicios HTTP y contratos

- Toda llamada a LudikAPI debe pasar por la instancia de `src/lib/axios.js`, salvo que exista un requisito técnico documentado.
- Mantén endpoints y serialización en `src/services/`; no disperses llamadas Axios entre componentes.
- Los servicios devuelven datos útiles para la UI y delegan la normalización de fallos en `handleApiError`.
- No absorbas errores silenciosamente. La UI debe distinguir carga, éxito, vacío y error cuando esos estados sean posibles.
- Usa `REACT_APP_API_URL` para configurar la API. No agregues URLs de producción, tokens ni credenciales al código.
- Las restricciones de `PrivateRoute` mejoran la experiencia, pero no constituyen seguridad. Toda acción protegida debe seguir autorizada por LudikAPI.

## Diseño, CSS y accesibilidad

- Prefiere un archivo `*.module.css` junto al componente para estilos locales. Añade reglas globales sólo cuando sean tokens, resets, animaciones o primitivas realmente compartidas.
- Reutiliza las clases globales de botones y las variables visuales existentes. No dupliques colores, espaciados o patrones sin revisar primero los estilos cercanos.
- Mantén la interfaz responsive y comprueba, como mínimo, vistas móviles y de escritorio cuando cambie el layout.
- Usa HTML semántico. Cada campo necesita una etiqueta asociada; cada imagen, un `alt` apropiado; cada botón de icono, un nombre accesible.
- Todas las acciones deben poder realizarse con teclado. Conserva indicadores de foco visibles y administra el foco al abrir o cerrar modales.
- No comuniques información sólo mediante color. Mantén contraste legible y respeta `prefers-reduced-motion` en animaciones no esenciales.
- Evita `dangerouslySetInnerHTML`. Si un caso existente obliga a usarlo, acepta únicamente contenido confiable o sanitizado y agrega una prueba contra contenido malicioso.
- Los mensajes visibles para usuarios permanecen en español y deben ser claros, breves y consistentes con el tono existente.

## Seguridad y privacidad

- Nunca agregues al repositorio secretos, tokens reales, credenciales, datos personales ni archivos `.env` con valores sensibles.
- No registres tokens JWT, credenciales, respuestas de seguridad ni respuestas completas que puedan contener datos personales.
- `redux-persist` utiliza almacenamiento del navegador y actualmente persiste autenticación. No agregues nuevos datos sensibles al estado persistido. Un cambio en el almacenamiento del token requiere una decisión coordinada con el backend.
- No confíes en validación del cliente para reglas de autorización o integridad; considérala una ayuda de UX y replica la garantía en la API.
- Trata texto, URLs, archivos y SVG provenientes del usuario o del servidor como no confiables. Evita HTML sin sanitizar y protocolos de URL peligrosos.
- No muestres detalles internos de Axios, stack traces ni respuestas técnicas crudas en notificaciones al usuario.

## Pruebas y validación

Cada cambio de comportamiento debe incluir pruebas proporcionales al riesgo. Prueba lo que observa la persona usuaria, no detalles internos de implementación.

- Usa React Testing Library con consultas por rol, nombre, etiqueta o texto accesible. Evita selectores atados a clases CSS o estructura accidental.
- Simula la frontera HTTP; no hagas llamadas reales a LudikAPI en pruebas unitarias de componentes o hooks.
- Incluye casos de carga, error, ausencia de datos y permisos cuando sean relevantes para la funcionalidad.
- Para correcciones, agrega una prueba de regresión que falle antes del cambio y pase después.
- La prueba `src/App.test.js` proviene de la plantilla y puede estar desactualizada respecto de la aplicación; no asumas que representa una línea base válida. Si una dependencia de Testing Library falta, informa el problema y no alteres dependencias fuera del alcance solicitado.
- No debilites aserciones, ocultes warnings relevantes ni uses snapshots extensos para lograr una suite verde.

Validación base desde `ludikclient/`:

```powershell
npm ci
npm run lint
$env:CI = "true"
npm test -- --watchAll=false
```

El script actual de `build` usa asignación de variables con sintaxis POSIX y funciona en el runner Linux de CI. En PowerShell, valida la compilación con:

```powershell
$env:CI = "false"
.\node_modules\.bin\react-scripts.cmd build
Remove-Item Env:CI
```

En Linux, macOS, Git Bash o CI puede utilizarse:

```bash
npm run build
```

Ejecuta primero la comprobación más acotada durante la iteración y completa lint, pruebas relevantes y build antes de entregar cambios de producción. Si una comprobación falla por un defecto previo, registra el comando y el error con precisión.

## Criterios de finalización

Antes de dar una tarea por terminada:

1. Revisa el diff completo y confirma que sólo contiene archivos relacionados.
2. Ejecuta lint, pruebas relevantes y build en proporción al cambio.
3. Comprueba manualmente el flujo afectado, incluidos carga, vacío, error, permisos y navegación cuando apliquen.
4. Verifica responsive, navegación por teclado, foco, etiquetas y contraste en cambios visuales.
5. Confirma que las query keys, invalidaciones, endpoints y payloads coinciden con LudikAPI.
6. Actualiza pruebas, documentación y configuración de ejemplo relacionadas.
7. Informa brevemente qué cambió, qué comandos se ejecutaron y sus resultados. Declara toda validación omitida, limitación o riesgo pendiente.
