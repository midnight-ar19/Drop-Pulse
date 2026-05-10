using System.Globalization;
using System.IO.Ports;
using DropPulse.Models;


namespace DropPulse.Services;

public class SensorService : IDisposable
{
    private readonly SerialPort _serial;
    private readonly Random _random = new();

    private readonly object _lock = new();
    private SensorData? _last;

    public SensorService()
    {
        _serial = new SerialPort("COM4", 9600)
        {
            NewLine = "\n",
            ReadTimeout = 5000
        };
        _serial.Open();

        _ = Task.Run(ListenLoop);
    }

    private async Task ListenLoop()
    {
        while (true)
        {
            try
            {
                var line = _serial.ReadLine();
                var parts = line.Split(',');

                if (parts.Length >= 3 &&
                    double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var t) &&
                    double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var h) &&
                    double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out var s))
                {
                    var data = new SensorData
                    {
                        Timestamp = DateTime.UtcNow,
                        Temperature = t,
                        AirHumidity = h,
                        SoilMoisture = s
                    };

                    lock (_lock) _last = data;
                }
            }
            catch
            {
                await Task.Delay(500); // evita bucle agresivo en error
            }
        }
    }

    public SensorData? GetLatest()
    {
        lock (_lock) return _last;
    }

    public void Dispose()
    {
        if (_serial.IsOpen) _serial.Close();
        _serial.Dispose();
    }
}