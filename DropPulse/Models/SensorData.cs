using System;
using System.Collections.Generic;

namespace DropPulse.Models;

public partial class SensorData
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public double SoilMoisture { get; set; }
    public double AirHumidity { get; set; }
    public double Temperature { get; set; }
}
