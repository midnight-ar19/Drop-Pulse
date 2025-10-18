using System.Text.Json.Serialization;

namespace DropPulse.Models
{
    public class AiResponse
    {
        [JsonPropertyName("decision")]
        public string Decision { get; set; }

        [JsonPropertyName("received_data")]
        public ReceivedDataModel ReceivedData { get; set; }
    }

    public class ReceivedDataModel
    {
        [JsonPropertyName("soilMoisture")]
        public double SoilMoisture { get; set; }

        [JsonPropertyName("airHumidity")]
        public double AirHumidity { get; set; }

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }
    }
}
