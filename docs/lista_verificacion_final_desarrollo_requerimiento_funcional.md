# **Lista de Verificación** Para Final de Cada Funcionalidad

## 1. Estado de compilación y errores

- [ ] Compila sin errores ni *warnings* (trátalos como errores).
- [ ] No hay *warnings* suprimidos en el proyecto.
- [ ] La funcionalidad implementada funciona según lo esperado.

## 2. Calidad y estilo de código

- [ ] Se respetan convenciones de estilo (nombres de clases y métodos en PascalCase, nombres de métodos y atributos en camelCase, indentación, etc).
- [ ] No hay código muerto ni duplicado; se extrae lógica común.
- [ ] No hay `Console.WriteLine`, bloques de depuración ni código comentado innecesario.

## 3. Arquitectura y mantenibilidad

- [ ] Separación de capas: API ↔ Aplicación ↔ Dominio ↔ Infraestructura, con dependencias unidireccionales.
- [ ] Las interfaces están en la capa de Aplicación; las implementaciones, en Infraestructura.
- [ ] Se inyectan dependencias vía contenedor (no `new` en controladores).
- [ ] Se parametriza con abstracciones (interfaces/abstractas) en lugar de clases concretas.

## 4. Pruebas

- [ ] Cobertura ≥ 80 % en dominio y aplicación.
- [ ] Cada caso de uso tiene tests unitarios (positivos/negativos), usando mocks para infraestructuras externas.

## 5. Documentación y API

- [ ] Controladores y casos de uso documentados con XML `<summary>` para Swagger.
- [ ] DTOs anotados (`[Required]`, `[MaxLength]`), sin “números mágicos”.
- [ ] Comentarios solo para aclarar el “por qué”, no el “cómo”.

## 6. Rendimiento y seguridad

- [ ] Validación de entrada centralizada (FluentValidation o DataAnnotations).
- [ ] Manejo de excepciones con middleware (`ProblemDetails`), sin impresiones directas.
- [ ] Autorización aplicada (`[Authorize]`, roles, claims).
- [ ] Paginación o streaming (`IAsyncEnumerable`) para colecciones grandes.
