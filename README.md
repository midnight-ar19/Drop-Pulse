# Drop Pulse
**Sistema Inteligente de Riego Simulado con .NET y Python**

Pulse Drop es un proyecto de demostración que simula un sistema de riego inteligente. Su objetivo es optimizar el uso del agua mediante la combinación de una aplicación web **ASP.NET Core MVC** con un microservicio de **Inteligencia Artificial en Python**.

El sistema genera datos de sensores ambientales (humedad, temperatura), los envía al servicio de IA para obtener una decisión, y visualiza todo el proceso en un dashboard en tiempo real.

---

### Características Principales
* **Visualización en Tiempo Real:** Un dashboard interactivo que muestra las métricas de los sensores con gráficos dinámicos.
* **Simulación de Datos:** Genera datos realistas de humedad del suelo, humedad del aire y temperatura.
* **Decisiones con IA:** Un microservicio en Python recibe los datos y determina si es necesario regar.
* **Registro Histórico:** Almacena y muestra un historial completo de todos los eventos de riego.
* **Panel de Configuración:** Permite ajustar los parámetros de la simulación, como los umbrales de humedad.

---

### 🏛️ Arquitectura Simplificada
El proyecto se compone de dos partes principales que se comunican a través de una API REST:

1.  **Aplicación Web (ASP.NET Core MVC):**
    * Gestiona la interfaz de usuario, la simulación de datos y la persistencia en la base de datos (SQL Server).
    * Envía los datos de los sensores al microservicio de IA.

2.  **Microservicio de IA (Python & FastAPI):**
    * Recibe los datos del sensor en un endpoint (`/predict`).
    * Aplica una lógica (simulando un modelo de IA) para tomar una decisión.
    * Devuelve una respuesta simple: `"Regar"` o `"No Regar"`.

---

### 💻 Pila Tecnológica

| Componente | Tecnología |
| :--- | :--- |
| **Aplicación Principal** | C#, ASP.NET Core 8 MVC, Entity Framework Core, Chart.js |
| **Microservicio de IA** | Python, FastAPI |
| **Base de Datos** | SQL Server |
