using System;
using System.Collections.Generic;

namespace DropPulse.Models;

public partial class Irrigation
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public bool WasIrrigationActivated { get; set; }
    public int SensorDataId { get; set; }
    public virtual required SensorData SensorData { get; set; }
}
