using System;
using System.Collections.Generic;

namespace DropPulse.Models;

public partial class IrrigationEvent
{
    public int Id { get; set; }

    public DateTime StartTime { get; set; }

    public int DurationSeconds { get; set; }

    public string TriggerReason { get; set; } = null!;
}
