### Lista de Verificación Determinista para Agentes

**1. Verificación de Compilación Estricta**

* [ ] Se ejecutó exitosamente el comando `dotnet build .\LudikAPI.sln --configuration Release --no-restore`.
* [ ] El código compila con cero advertencias; está prohibido ocultar advertencias de tipo `Nullable` mediante el operador `!` a menos que la invariancia esté matemáticamente demostrada en el código.
* [ ] Ningún cambio editó carpetas `bin/`, `obj/`, resultados de pruebas o archivos generados automáticamente.

**2. Integridad Arquitectónica y Manejo de Estado**

* [ ] El flujo de dependencias se mantiene intacto: el dominio no depende de la aplicación, y la API actúa únicamente como punto de composición.
* [ ] Los fallos esperables de la lógica de negocio se modelan estrictamente mediante el patrón `Resultado<T>` y `Error`, reservando las excepciones de C# exclusivamente para errores catastróficos o de infraestructura.
* [ ] No se instanció directamente `ContextoDb` en controladores o casos de uso; se respetaron los contratos de repositorio existentes.

**3. Evidencia de Pruebas (Definition of Done)**

* [ ] Se ejecutó `dotnet test .\PruebasUnitarias\PruebasUnitarias.csproj --configuration Release --no-build` y el resultado es 100% verde.
* [ ] Para correcciones de errores, se escribió una prueba de regresión que se verificó que fallaba antes del cambio y que pasa exitosamente después de la modificación.
* [ ] Las aserciones en las pruebas verifican resultados observables y cambios de estado (pre y pos condición), no la implementación interna.

**4. Contratos Públicos y Seguridad**

* [ ] Si se modificó un endpoint, se actualizaron obligatoriamente los comentarios XML y los metadatos de Swagger correspondientes al nuevo comportamiento público.
* [ ] La autorización se verifica leyendo los *claims* directamente en el servidor; bajo ninguna circunstancia se confía en identificadores de usuario o roles enviados en el cuerpo JSON por el cliente.
* [ ] Los errores HTTP utilizan el manejo centralizado de `WebApi/Helpers` y no exponen *stack traces* ni mensajes internos al cliente.