# Documentación de Requerimientos Funcionales

Para facilitar la indexación, búsqueda y comprensión por parte de un agente de inteligencia artificial (RAG/LLMs), los requerimientos han sido agrupados por **Módulos Lógicos de Dominio**. Cada requerimiento mantiene una estructura estricta de clave-valor.

---

## 1. MÓDULO: SEGURIDAD, AUTENTICACIÓN Y GESTIÓN DE CUENTAS

### [RF1] Registro de alumnos
* **Prioridad:** Alta
* **Dependencias:** Ninguna
* **Proceso de negocio:** Registro
* **Descripción:** El sistema debe permitir a los alumnos registrarse con su nombre y apellido, un nombre de usuario único y contraseña.
* **Reglas de negocio:**
  - El nombre y apellido no puede incluir caracteres especiales ni números, además, deben de contener entre 3 y 20 caracteres. 
  - El nombre de usuario debe ser único y tener entre 3 y 20 caracteres. 
  - La contraseña debe tener al menos 8 caracteres que incluyan al menos una mayúscula, una minúscula, un número y un carácter especial.
  - La contraseña se almacenará de forma encriptada. Se utilizará el algoritmo de bcrypt.
  - En caso de no cumplir con ninguna de las reglas anteriores, se deberá de mostrar mensajes de error específicos para cada caso.

### [RF2] Autenticación de alumnos
* **Prioridad:** Alta
* **Dependencias:** Ninguna
* **Proceso de negocio:** Inicio de sesión
* **Descripción:** El sistema debe permitir a los alumnos autenticarse con sus credenciales (nombre de usuario y contraseña).
* **Reglas de negocio:**
  - Solo los alumnos registrados podrán acceder al sistema; se debe validar la autenticación antes de permitir el acceso a cualquier funcionalidad.
  - En caso de error se deberá de mostrar un mensaje de error genérico (para no revelar información sobre la existencia de otras cuentas).
  - Para el manejo de sesión se hará uso de Tokens JWT sin Expiración. Pudiendo en versiones posteriores del sistema implementar un mecanismo de renovación periódica del token en segundo plano para garantizar que siga siendo seguro.
  - Luego de 5 intentos fallidos para ingresar a la aplicación, sé bloquera por 30 minutos la posibilidad de loguearse.

### [RF5] Registro de profesores
* **Prioridad:** Alta
* **Dependencias:** Ninguna
* **Proceso de negocio:** Registro
* **Descripción:** El sistema debe permitir a los profesores registrarse con un correo único, nombre, apellido y contraseña.
* **Reglas de negocio:**
  - El correo debe ser único. Y los caracteres permitidos en la parte local (antes del @) son letras (a-z, sin distinción entre mayúsculas y minúsculas), números (0-9), y algunos caracteres especiales como puntos (.), guiones bajos (_) y guiones (-). Sin embargo, no se permiten caracteres no ASCII. 
  - La contraseña debe tener al menos 8 caracteres que incluyan al menos una mayúscula, una minúscula, un número y un carácter especial.
  - El nombre y apellido del usuario debe contener entre 3 y 20 caracteres.

### [RF6] Autenticación de profesores
* **Prioridad:** Alta
* **Dependencias:** Ninguna
* **Proceso de negocio:** Inicio de sesión
* **Descripción:** El sistema debe permitir a los profesores autenticarse con sus credenciales (correo electrónico y contraseña).
* **Reglas de negocio:**
  - Solo los profesores registrados podrán acceder al sistema.
  - En caso de error se deberá de mostrar un mensaje de error genérico (para no revelar información sobre la existencia de otras cuentas).
  - Para el manejo de sesión se hará uso de Tokens JWT sin Expiración. Pudiendo en versiones posteriores del sistema implementar un mecanismo de renovación periódica del token en segundo plano para garantizar que siga siendo seguro.
  - Luego de 5 intentos fallidos para ingresar a la aplicación, sé bloquera por 30 minutos la posibilidad de loguearse.

### [RF7] Recuperación de contraseña de alumnos
* **Prioridad:** Media
* **Dependencias:** RF2 (Autenticación de alumnos)
* **Proceso de negocio:** Gestión de cuentas
* **Descripción:** El sistema debe permitir a los profesores gestionar la recuperación de contraseñas de las cuentas de alumnos mediante un sistema de solicitud de PIN. El alumno debe presencialmente solicitar la recuperación de la contraseña a un docente. El docente en el perfil del estudiante tendrá una opción que será restaurar contraseña, al seleccionarla se generara un PIN temporal que sustituirá la contraseña actual del estudiante. Se le solicitará al estudiante que cambie el PIN (su contraseña actual) por una contraseña que cumpla los requerimientos, luego que ingrese por primera vez a la aplicación tras aplicarse la sustitución de su contraseña (no recordada) por el PIN.
* **Reglas de negocio:**
  - Solo el profesor del grupo puede generar un PIN para la recuperación de cuenta de un alumno.
  - El PIN es un código numérico de 6 cifras.
  - El PIN durará hasta que el estudiante ingrese a la aplicación y cambie su contraseña.

### [RF8] Recuperación de contraseña de profesores
* **Prioridad:** Media
* **Dependencias:** RF6 (Autenticación de profesores)
* **Proceso de negocio:** Gestión de cuentas de profesores
* **Descripción:** El sistema debe permitir a los profesores recuperar el acceso a sus cuentas mediante un proceso estructurado que incluya solicitud de recuperación, generación/envío de PIN y ejecución del restablecimiento.
* **Reglas de negocio:**
  - Solicitud de recuperación: El profesor inicia el proceso desde la pantalla de inicio de sesión, ingresando su correo electrónico registrado. El sistema válida que el correo exista en la base de datos y esté asociado a un rol de profesor.
  - Generación y envío de PIN: El sistema genera un PIN único con expiración en una hora. Se envía un PIN de recuperación al correo del profesor.
  - Ejecución del restablecimiento: El sistema solicita el PIN. Verifica la validez del PIN (no expirado y no utilizado previamente). El profesor ingresa una nueva contraseña, cumpliendo requisitos de complejidad definidos. El sistema actualiza la contraseña en la base de datos (almacenada como hash seguro) y revoca el PIN usado.
  - El correo electrónico debe estar previamente registrado y verificado.
  - El PIN debe expirar tras 1 hora y ser válido para una única operación.
  - En caso de que el PIN sea inválido o haya expirado, se redirige al usuario a la pantalla de solicitud y se muestra un mensaje de error.
  - Cuando la contraseña sea cambiada de forma exitosa, se brindará un mensaje de éxito.

### [RF15] Cerrar sesión
* **Prioridad:** Alta
* **Dependencias:** RF2 (Autenticación de alumnos) y RF6 (Autenticación de profesores)
* **Proceso de negocio:** Potenciadores (Nota: clasificado originalmente así en el texto, funcionalmente corresponde a Seguridad)
* **Descripción:** El sistema debe de permitir a los usuarios cerrar sesión. Al cerrar sesión se redirige al usuario a la pantalla de login.
* **Reglas de negocio:**
  - Un usuario debería de estar autenticado antes de poder cerrar sesión.
  - Al cerrar sesión se borra el Token JWT.

### [RF28] Recuperación de contraseña de alumnos mediante preguntas de seguridad
* **Prioridad:** Media
* **Dependencias:** RF1 (Registro de alumnos) y RF2 (Autenticación de alumnos)
* **Proceso de negocio:** Gestión de cuentas (Recuperación)
* **Descripción:** El sistema debe permitir a los alumnos recuperar su contraseña mediante la validación de respuestas a preguntas de seguridad configuradas durante el registro, sin requerir correo electrónico o número de celular.
* **Reglas de negocio:**
  - Durante el registro, el alumno deberá seleccionar y responder al menos dos (idealmente tres) preguntas de seguridad de una lista predefinida o personalizadas.
  - Las respuestas se almacenarán normalizadas (por ejemplo, en minúsculas y sin acentos) para asegurar la correcta comparación en el momento de recuperación.
  - Para iniciar la recuperación, el alumno se identificará mediante su nombre de usuario; el sistema presentará de manera aleatoria dos de las preguntas configuradas.
  - El sistema comparará las respuestas ingresadas con las almacenadas, ignorando diferencias en mayúsculas, minúsculas o acentos.
  - Se permitirá un máximo de tres intentos para la validación; de superarse este límite, el proceso de recuperación se bloqueará temporalmente.
  - En caso de validación exitosa, se permitirá al alumno establecer una nueva contraseña inmediatamente, notificándole el cambio exitoso.

---

## 2. MÓDULO: GESTIÓN DE GRUPOS Y PERFILES

### [RF3] Unión a grupos
* **Prioridad:** Alta
* **Dependencias:** RF9 (Creación y gestión de grupos)
* **Proceso de negocio:** Creación y gestión de grupos
* **Descripción:** El sistema debe permitir a los alumnos unirse a grupos mediante códigos QR o enlaces únicos.
* **Reglas de negocio:**
  - Un alumno solo puede unirse a grupo si está autenticado. 
  - Al leer un código QR o seleccionar el enlace será dirigido a la aplicación y se le consultará si desea unirse al grupo, en caso de no está autenticado se le pedirá que lo haga antes de mostrarle la opción de unirse al grupo. 
  - El profesor podrá aceptar o no que un alumno se una al grupo.
  - Un alumno no se puede unir más de una vez al mismo grupo. 
  - El código QR o enlace único solo será válido mientras exista el grupo. Es decir, funcionará y permitirá unirse al grupo mientras el profesor no elimine el grupo.
  - En caso de que el QR y enlace no sea válido, se mostrara en pantalla un mensaje de error que indica que el QR y enlace han expirado.

### [RF4] Personalización del perfil de alumno en un grupo
* **Prioridad:** Media
* **Dependencias:** RF9 (Creación y gestión de grupos)
* **Proceso de negocio:** Gestión de perfiles de usuario
* **Descripción:** El sistema debe permitir a los alumnos personalizar su perfil dentro de cada grupo, incluyendo la opción de cambiar su imagen de perfil a partir de un banco de imágenes pre cargadas o comprar una imagen con las monedas que posea. Para editar el perfil dentro de un grupo, el estudiante debe de ingresar al grupo y seleccionar su imagen de grupo, se le abrirá una pantalla donde podrá elegir la opción de cambiar imagen.
* **Reglas de negocio:**
  - La personalización se limita a las opciones predefinidas por el profesor y las que trae el sistema por defecto.
  - Al comprar una imagen se le restará el valor de la imagen al monto total que tenga en su monedero.
  - En caso de no tener fondos suficientes para comprar una imagen, se brindará un mensaje de información: “No cuentas con monedas suficientes para obtener este artículo”.

### [RF9] Creación y gestión de grupos
* **Prioridad:** Alta
* **Dependencias:** RF6 (Autenticación de profesores)
* **Proceso de negocio:** Creación y administración de grupos
* **Descripción:** El sistema debe permitir a los profesores crear, modificar y eliminar grupos. Para la creación de un grupo se solicita un nombre y una tabla de equivalencia, la cual puede seleccionar de las tablas pre cargadas en el sistema o creadas anteriormente por el docente. Cuando se crea el grupo se genera el código QR y el enlace para unirse al grupo. La información para unirse al grupo queda almacena para poder consultarla más tarde.
* **Reglas de negocio:**
  - Un profesor solo puede gestionar los grupos que haya creado. 
  - Un mismo docente no puede crear dos grupos con el mismo nombre.
  - Se le debe brindar un mensaje de aviso al profesor cuando intente crear o modificar un grupo con un nombre repetido.
  - Se debe de almacenar la fecha de generación del grupo, el código QR y el enlace.
  - El profesor puede acceder al código QR y enlace de unión al grupo en la pantalla de información del grupo. 

### [RF10] Generación de códigos de invitación
* **Prioridad:** Alta
* **Dependencias:** RF9 (Creación y gestión de grupos)
* **Proceso de negocio:** Gestión de grupos
* **Descripción:** El sistema debe generar un código QR y un enlace único al crear un grupo, para que los alumnos puedan unirse. La información (QR y enlace) debe almacenarse para consultas posteriores.
* **Reglas de negocio:**
  - El código de invitación (QR y enlace) es válido mientras el grupo esté activo y no se desactive manualmente.

### [RF11] Pantalla de información del profesor
* **Prioridad:** Media
* **Dependencias:** RF9 (Creación y gestión de grupos) y RF16 (Creación de medallas)
* **Proceso de negocio:** Gestión de grupos y evaluación de alumnos
* **Descripción:** El sistema debe proporcionar a los profesores una pantalla para ver el listado de alumnos de un grupo, incluyendo para cada alumno: nombre, apellido, meta de calificación, medallas asignadas y su conversión en nota. Se deben de mostrar todos los alumnos del grupo en la misma página (no se quiere paginado).
* **Reglas de negocio:**
  - Solo el profesor puede visualizar la información de sus grupos.
  - La nota del alumno es hallada en función de la tabla de equivalencia establecida por el profesor y las medallas que posee el alumno.

---

## 3. MÓDULO: GAMIFICACIÓN - EVALUACIÓN Y MEDALLAS

### [RF12] Creación y gestión de tabla de equivalencia (GPS de Calificaciones)
* **Prioridad:** Alta
* **Dependencias:** RF16 (Creación de medallas)
* **Proceso de negocio:** Evaluación de alumnos
* **Descripción:** El sistema debe permitir a los profesores crear y editar una tabla que asocie un valor numérico a un conjunto determinado y finito de medallas. El profesor deberá asignar manualmente las medallas que tiene asociado cada valor, para esto contara con un select múltiple que le mostrara todas las medallas disponibles (precargas y creadas por el profesor).
* **Reglas de negocio:**
  - Las tablas de equivalencia deben ser configurables por cada profesor.
  - Cada valor de la tabla representa una nota y este no puede estar repetido.
  - Los valores son progresivos, es decir, para obtener nota X se deben cumplir las condiciones de X-1.
  - El profesor solo podrá iniciar a completar por el valor más bajo e irá pudiendo completar hacia los valores más altos.
  - Cuando el profesor establezca para una nota una medalla base, todas las siguientes notas deberán tener esa como base y no podrá ser modificada de manera manual, solo se podrán agregar medallas a la "base".
  - Si el profesor modifica una equivalencia ya establecida, se debe modificar todas las equivalencias siguientes con el nuevo valor base.

### [RF13] Visualización de medallas
* **Prioridad:** Alta
* **Dependencias:** RF16 (Creación de medallas por el profesor)
* **Proceso de negocio:** Evaluación y motivación
* **Descripción:** El sistema debe permitir a los alumnos visualizar las medallas obtenidas por cada grupo, incluyendo el conteo de medallas repetidas. 
* **Reglas de negocio:**
  - El alumno se encuentra autenticado.
  - El alumno podrá ver las medallas que ha ganado en un grupo accediendo al grupo y luego en la pantalla de resumen tendrá qué seleccionar “medallas”.

### [RF14] Asignación de medallas entre alumnos
* **Prioridad:** Media
* **Dependencias:** RF16 (Creación de medallas por el profesor)
* **Proceso de negocio:** Evaluación y motivación
* **Descripción:** El sistema debe permitir a los alumnos otorgar medallas a otros compañeros. Las medallas permitidas están reducidas a aquellas de asignación mutua. Para asignar, el alumno va al listado de integrantes, selecciona el botón de otorgar medalla al lado del nombre del compañero, elige una medalla y confirma.
* **Reglas de negocio:**
  - Cada medalla de asignación mutua solo puede ser entregada un número limitado de veces, definido por el profesor.
  - El alumno debe de estar autenticado.
  - Cuando se asigna la medalla al compañero se muestra un mensaje de éxito, en caso de fallo, se advierte el fallo.
  - El receptor recibirá la medalla sin saber qué compañero se la brindo.

### [RF16] Creación de medallas
* **Prioridad:** Alta
* **Dependencias:** RF9 (Creación y gestión de grupos)
* **Proceso de negocio:** Evaluación y gamificación
* **Descripción:** El sistema debe permitir a los profesores crear y modificar medallas. Para ello se debe seleccionar una imagen, agregar un nombre, una descripción, establecer si es de asignación mutua y establecer cuántas monedas brinda la medalla al ser ganada. Si es de asignación mutua, no brindará monedas.
* **Reglas de negocio:**
  - La imagen podrá ser seleccionada de un banco de imágenes pre cargadas.
  - El nombre de la medalla debe ser único y contener entre 5 y 30 caracteres.
  - La descripción es opcional, con un máximo de hasta 50 caracteres. 
  - La cantidad de monedas no puede ser negativa.
  - Validación de nombre único y longitud de caracteres con mensajes de error específicos.

### [RF17] Asignación de medallas a los alumnos (por profesor)
* **Prioridad:** Alta
* **Dependencias:** RF9 (Creación y gestión de grupos)
* **Proceso de negocio:** Evaluación y gamificación
* **Descripción:** El sistema debe permitir a los profesores asignar medallas a los alumnos. Al visualizar un listado de alumnos, el profesor podrá seleccionar la/las medallas. Cuando selecciona, se asigna sin confirmación extra.
* **Reglas de negocio:**
  - Un profesor solo puede asignar medallas a alumnos dentro de sus grupos.
  - No existe límite de cantidad de medallas del mismo tipo asignadas.
  - Mensaje de éxito tras la asignación.
  - Para asignar dos medallas iguales, el profesor presiona dos veces la misma medalla.

### [RF18] Quitar medallas a los alumnos (por profesor)
* **Prioridad:** Alta
* **Dependencias:** RF9 (Creación y gestión de grupos)
* **Proceso de negocio:** Evaluación y gamificación
* **Descripción:** El sistema debe permitir a los profesores quitar medallas a los alumnos sin confirmación extra tras la selección.
* **Reglas de negocio:**
  - Solo a alumnos de sus grupos.
  - Solo puede quitar medallas que el alumno ha ganado (no saldos negativos).
  - Mensaje de éxito tras la acción.
  - Para quitar dos medallas iguales, se presiona dos veces.

### [RF25] Reinicio de logros de grupo o grupos
* **Prioridad:** Alta
* **Dependencias:** RF9 (Creación y gestión de grupos)
* **Proceso de negocio:** Reinicio de logros de grupos
* **Descripción:** El sistema debe permitir a los profesores reiniciar todas las medallas obtenidas por los alumnos de un grupo o de todos los grupos. Antes de reiniciar, se debe almacenar la nota y medallas de cada estudiante en ese periodo histórico.
* **Reglas de negocio:**
  - Solo los grupos creados por él.
  - Profesor autenticado.
  - Se pide confirmación antes de la eliminación.
  - Los logros del periodo anterior estarán disponibles en la sección "periodos anteriores" del perfil del alumno.
  - Se debe notificar a los estudiantes tras el reinicio.

---

## 4. MÓDULO: GAMIFICACIÓN - TIENDA, RECOMPENSAS Y POTENCIADORES

### [RF19] Canje de recompensas
* **Prioridad:** Media
* **Dependencias:** RF21 (Creación y gestión de recompensas por parte del profesor)
* **Proceso de negocio:** Gamificación y motivación
* **Descripción:** El sistema debe permitir a los alumnos canjear recompensas por las monedas obtenidas (ej: avatares, recompensas del profesor específicas del grupo).
* **Reglas de negocio:**
  - Solo recompensas disponibles en el catálogo del profesor.
  - Mensaje de error por falta de fondos.
  - Objetos de personalización comprados se bloquean en tienda y pasan a estar disponibles al editar perfil.
  - Recompensas del profesor van al inventario; el alumno debe seleccionar “utilizar recompensa” (se oculta para él y se vuelve visible para el profesor).
  - El profesor puede ver el inventario del alumno ordenado por fecha decreciente.
  - Alumno autenticado.

### [RF20] Creación y gestión de recompensas en la tienda por parte del profesor
* **Prioridad:** Media
* **Dependencias:** RF6 (Autenticación de profesor)
* **Proceso de negocio:** Gestión de recompensas
* **Descripción:** El sistema debe permitir a los profesores crear, modificar y eliminar recompensas canjeables por monedas (ej: una respuesta menos en un escrito).
* **Reglas de negocio:**
  - Nombre único, icono y costo en monedas.
  - Validación de nombre único con mensaje de error.
  - Mensaje de éxito tras creación.
  - No hay límite de cantidad de compras por recompensa por parte del alumno, regulado implícitamente por el balance de monedas.

### [RF26] Hitos
* **Prioridad:** Media
* **Dependencias:** RF17 (Asignación de medallas a los alumnos)
* **Proceso de negocio:** Hitos
* **Descripción:** El sistema debe incluir hitos a los cuales el estudiante puede llegar. Recibe una recompensa única o potenciador. Son a nivel global (suman medallas de cualquier grupo).
* **Reglas de negocio:**
  - Un alumno solo puede llegar a un hito una vez.
  - Alumno autenticado.
  - Cuentan todas las medallas ganadas, sin importar el tipo.

### [RF27] Potenciador de monedas
* **Prioridad:** Media
* **Dependencias:** RF26 (Hitos)
* **Proceso de negocio:** Potenciadores
* **Descripción:** El sistema brindará un potenciador de monedas por un tiempo limitado al alcanzar un hito (ej. duplica monedas ganadas).
* **Reglas de negocio:**
  - Tiempo de uso limitado (ej: 3 días).
  - Alumno autenticado.
  - Desactivación automática informada al alumno al terminar el tiempo.
  - Notificaciones de inicio y expiración.

---

## 5. MÓDULO: GAMIFICACIÓN - METAS Y RANKINGS

### [RF21] Visualización de rankings
* **Prioridad:** Media
* **Dependencias:** RF22 (Creación y gestión de rankings)
* **Proceso de negocio:** Gamificación y motivación
* **Descripción:** El sistema debe permitir a los alumnos visualizar las tablas de clasificación de su grupo, mostrando 2 posiciones arriba y 2 abajo, incluyendo nombre, apellido, avatar y puntos.
* **Reglas de negocio:**
  - Un alumno solo puede ver el ranking de su grupo.

### [RF22] Creación y gestión de rankings
* **Prioridad:** Media
* **Dependencias:** RF18 (Creación de medallas) *Nota: Posible typo en original, refiere a RF16*
* **Proceso de negocio:** Gestión de gamificación
* **Descripción:** El sistema debe permitir a los profesores crear tablas de clasificación basadas en las distintas medallas disponibles, creadas dentro de cada grupo.
* **Reglas de negocio:**
  - Funcionan por cantidad de medallas obtenidas.
  - Selección de la medalla a clasificar y confirmación.
  - Eliminación mediante botón.
  - Mensaje de éxito en creación o eliminación.
  - Desempate alfabético por nombre.

### [RF23] Establecimiento de metas personales
* **Prioridad:** Media
* **Dependencias:** RF12 (Creación y gestión de tabla de equivalencia)
* **Proceso de negocio:** Autoevaluación y motivación
* **Descripción:** El sistema debe permitir a los alumnos seleccionar una meta de la nota a alcanzar en el curso. Por defecto, nota máxima definida en el GPS.
* **Reglas de negocio:**
  - La meta debe estar dentro del rango definido en la tabla de equivalencia.
  - Alumno autenticado.

### [RF24] Visualización de barra de progreso
* **Prioridad:** Media
* **Dependencias:** RF23 (Establecimiento de metas personales)
* **Proceso de negocio:** Autoevaluación y motivación
* **Descripción:** El sistema debe mostrar una barra de progreso que indique la situación actual del alumno y el siguiente paso (medallas necesarias) hacia su meta. Dividida en niveles según la meta de calificación.
* **Reglas de negocio:**
  - Actualización al ingresar al grupo.
  - Progreso calculado en base a la tabla de equivalencia del profesor.
  - Inicia en nota mínima y termina en la meta seleccionada.
  - Muestra progreso parcial dentro de cada etapa (incremento visual con cada medalla obtenida hacia el siguiente nivel).
  - Cada nivel se marca visualmente.
  - Alumno autenticado.
