# Documentación de Requerimientos No Funcionales

Para mantener la consistencia con los requerimientos funcionales y facilitar su indexación por parte de un agente de inteligencia artificial (RAG/LLMs), los requerimientos no funcionales han sido agrupados por su **Tipo** (Categoría de Arquitectura de Software).

---

## 1. CATEGORÍA: RENDIMIENTO

### [RNF 1] Gestión de concurrencia
* **Tipo:** Rendimiento
* **Descripción:** El sistema debe gestionar múltiples usuarios simultáneos sin degradación de rendimiento.

### [RNF 3] Tiempo de respuesta en asignación de medallas
* **Tipo:** Rendimiento
* **Descripción:** El sistema debe permitir que el profesor asigne, bajo condiciones normales de uso, una medalla en menos de 3 segundos. Se asume que el profesor ya está dentro de la aplicación.

---

## 2. CATEGORÍA: USABILIDAD

### [RNF 2] Idioma del sistema
* **Tipo:** Usabilidad
* **Descripción:** El sistema utilizará en sus primeras versiones únicamente el idioma español en su interfaz de usuario. 

### [RNF 4] Experiencia de usuario en móviles
* **Tipo:** Usabilidad
* **Descripción:** El sistema debe resultar intuitivo y fácil de utilizar en dispositivos móviles.

---

## 3. CATEGORÍA: PORTABILIDAD

### [RNF 5] Diseño responsivo (Responsive Design)
* **Tipo:** Portabilidad
* **Descripción:** El sistema deberá ser adaptable a los diferentes anchos de pantallas de dispositivos móviles (celulares y tablets) para que su visualización sea correcta. 

### [RNF 6] Compatibilidad de Sistemas Operativos
* **Tipo:** Portabilidad
* **Descripción:** El sistema debe funcionar correctamente en diferentes sistemas operativos (iOS y Android).
