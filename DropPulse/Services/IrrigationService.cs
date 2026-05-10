using DropPulse.Models;

namespace DropPulse.Services
{
    public class IrrigationService
    {
        private readonly SensorService _sensorService;
        private readonly AiClientService _aiClientService;
        private readonly DroppulseContext _context;
        private readonly ILogger<IrrigationService> _logger;

        public IrrigationService(
            SensorService sensorService,
            AiClientService aiClientService,
            DroppulseContext context,
            ILogger<IrrigationService> logger)
        {
            _sensorService = sensorService;
            _aiClientService = aiClientService;
            _context = context;
            _logger = logger;
        }

        public async Task ProcessAndStoreIrrigationDecision()
        {
            var sensorData = _sensorService.GetLatest();

            if (sensorData == null)
            {
                _logger.LogWarning("Aún no hay datos del Arduino.");
                return;
            }
            _logger.LogInformation("Datos de sensor generados.");

            var aiResponse = await _aiClientService.GetPredictionAsync(sensorData);

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
