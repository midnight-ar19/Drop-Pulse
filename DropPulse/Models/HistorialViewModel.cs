namespace DropPulse.Models
{
    public class HistorialViewModel
    {
        public int ID { get; set; }
        public double HumedadSuelo { get; set; }
        public double HumedadAire { get; set; }
        public double Temperatura { get; set; }
        public string Regado { get; set; } // "Sí" o "No"
    }
}
