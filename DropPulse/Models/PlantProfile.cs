using System;
using System.Collections.Generic;

namespace DropPulse.Models;

public partial class PlantProfile
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double MinSoilMoistureThreshold { get; set; }

    public double MaxSoilMoistureThreshold { get; set; }
}
