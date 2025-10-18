using DropPulse.Models;
using DropPulse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DropPulse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IrrigationService _irrigationService;
        private readonly DroppulseContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            IrrigationService irrigationService,
            DroppulseContext context 
            )
        {
            _logger = logger;
            _irrigationService = irrigationService;
            _context = context;
        }

        public async Task<IActionResult> TriggerIrrigationCheck()
        {
            await _irrigationService.ProcessAndStoreIrrigationDecision();
            TempData["Message"] = "Ciclo de riego procesado y guardado con éxito.";
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {

            // 3. CONSULTAR LA BASE DE DATOS
            var historial = _context.Irrigation // Empezamos desde la tabla de Riegos
                .Include(i => i.SensorData)     // ¡Crucial! Incluimos los datos del sensor relacionados
                .OrderByDescending(i => i.Id)   // Ordenamos para mostrar los más nuevos primero
                .Take(10)                       // Tomamos solo los últimos 10 para no sobrecargar la tabla
                .Select(i => new HistorialViewModel // 4. MAPEAMOS al ViewModel
                {
                    ID = i.Id,
                    HumedadSuelo = i.SensorData.SoilMoisture,
                    HumedadAire = i.SensorData.AirHumidity,
                    Temperatura = i.SensorData.Temperature,
                    // Usamos un operador ternario para convertir el bool a string
                    Regado = i.WasIrrigationActivated ? "Sí" : "No"
                })
                .ToList(); // Ejecutamos la consulta y la convertimos en una lista

            // 5. PASAMOS LA LISTA A LA VISTA
            return View(historial);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
