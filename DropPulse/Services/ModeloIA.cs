using DropPulse.Models;

namespace DropPulse.Services
{
    public class ModeloIA
    {
        public bool DecidirSiRiega(SensorData datos)
        {
            // Si la humedad del suelo es menor a 30, se riega
            return datos.SoilMoisture < 30;
        }
    }
}

