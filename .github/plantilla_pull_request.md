# Lista de verificación para Pull Request

## 1. Descripción y contexto
- [ ] ¿El título del PR es claro y descriptivo?  
- [ ] ¿La descripción explica el propósito del cambio y su contexto?  

## 2. Calidad del código

### Convenciones y estilo
- [ ] Verificar linters (ESLint, StyleCop, RuboCop) sin errores.  
- [ ] Alineación de indentación, espacios y tabuladores uniformes.   

### Legibilidad y nomenclatura
- [ ] Variables y funciones con nombres self-documenting (e.g. `calcularTotal()` vs `ct()`).  
- [ ] Evitar abreviaturas oscuras; preferir `userAge` en lugar de `uA`.  
- [ ] Agrupar propiedades relacionadas en objetos o estructuras.  

### Modularidad y responsabilidad única
- [ ] Cada función/módulo cumple un único propósito (Single Responsibility).  
- [ ] Máximo ~30 líneas por función para facilitar pruebas y mantenimiento.  
- [ ] Validar que no existan “God objects” o clases que hagan todo.  

### Complejidad y acoplamiento
- [ ] Evitar dependencias circulares y duplicación (DRY).  
- [ ] Usar inyección de dependencias para facilitar mocks en tests.  

### Código muerto y comentarios
- [ ] Eliminar bloques comentados o variables no usadas.  
- [ ] Comentarios solo para explicar “por qué”, no “qué”.  
- [ ] Documentar intenciones complejas o algoritmos no triviales.  

### Performance y recursos
- [ ] Detectar loops costosos o recursiones profundas.  
- [ ] Revisar uso de memoria en colecciones grandes.  

## 3. Pruebas y validación

### Cobertura y alcance
- [ ] Cobertura mínima del 80 % de líneas críticas; priorizar lógica de negocio.  
- [ ] Tests para rutas felices, bordes (nulos, límites de arrays) y casos de error.  

### Tipos de pruebas
- [ ] Unitarias: aislar funciones con mocks/stubs a dependencias externas.  
- [ ] Integración: interacción entre módulos, base de datos o API externas.  
- [ ] Contract tests: asegurar que clientes y servicios coincidan en contratos (Pact).  

### Revisión de resultados
- [ ] Validar logs de test en busca de warnings o errores silenciosos.  
- [ ] Revisar flakiness: tests que fallan intermitentemente deben corregirse o marcarse.   