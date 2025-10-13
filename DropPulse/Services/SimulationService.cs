using DropPulse.Models;

namespace DropPulse.Services
{
    public class SimulationService
    {
        private readonly Random _random = new();

        public SensorDatum GenerateSensorData()
        {
            var data = new SensorDatum
            {
                Timestamp = DateTime.UtcNow,

                SoilMoisture = _random.NextDouble() * (50 - 5) + 5, 
                AirHumidity = _random.NextDouble() * (90 - 30) + 30,  
                Temperature = _random.NextDouble() * (45 - 15) + 15   
            };

            return data;
        }
    }
}
