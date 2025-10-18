using DropPulse.Models;

namespace DropPulse.Services
{
    public class IrrigationService
    {
        private readonly SimulationService _simulationService;
        private readonly AiClientService _aiClientService;
        private readonly DroppulseContext _context;
        private readonly ILogger<IrrigationService> _logger;

        public IrrigationService(
            SimulationService simulationService,
            AiClientService aiClientService,
            DroppulseContext context,
            ILogger<IrrigationService> logger)
        {
            _simulationService = simulationService;
            _aiClientService = aiClientService;
            _context = context;
            _logger = logger;
        }

        public async Task ProcessAndStoreIrrigationDecision()
        {
            var simulatedSensorData = _simulationService.GenerateSensorData();
            _logger.LogInformation("Datos de sensor generados.");

            var aiResponse = await _aiClientService.GetPredictionAsync(simulatedSensorData);

            if (aiResponse == null || aiResponse.ReceivedData == null)
            {
                _logger.LogError("No se recibió una respuesta válida del servicio de IA.");
                return;
            }

            _logger.LogInformation($"Decisión de la IA recibida: {aiResponse.Decision}");

            var sensorDataToStore = new SensorData
            {
                Timestamp = DateTime.UtcNow,
                SoilMoisture = aiResponse.ReceivedData.SoilMoisture,
                AirHumidity = aiResponse.ReceivedData.AirHumidity,
                Temperature = aiResponse.ReceivedData.Temperature
            };

            var irrigationRecord = new Irrigation
            {
                StartTime = DateTime.UtcNow,
                WasIrrigationActivated = aiResponse.Decision.Equals("Regar", StringComparison.OrdinalIgnoreCase),
                SensorData = sensorDataToStore
            };
            _context.Irrigation.Add(irrigationRecord);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Nuevo registro de riego con ID {irrigationRecord.Id} guardado en la base de datos.");
        }
    }
}
