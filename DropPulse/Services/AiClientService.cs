using DropPulse.Models;

namespace DropPulse.Services
{
    public class AiClientService
    {
        private readonly HttpClient _httpClient;

        public AiClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AiResponse> GetPredictionAsync(SensorData dataToSend)
        {
            var endpoint = "/predict";
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endpoint, dataToSend);

            if (response.IsSuccessStatusCode)
            {
                var aiResponse = await response.Content.ReadFromJsonAsync<AiResponse>();
                return aiResponse;
            }
            else
            {
                return null; 
            }
        }
    }
}
