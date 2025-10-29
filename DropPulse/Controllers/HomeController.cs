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
            var historial = _context.Irrigation 
                .Include(i => i.SensorData)     
                .OrderByDescending(i => i.Id)   
                .Take(10)                       
                .Select(i => new HistorialViewModel
                {
                    ID = i.Id,
                    HumedadSuelo = i.SensorData.SoilMoisture,
                    HumedadAire = i.SensorData.AirHumidity,
                    Temperatura = i.SensorData.Temperature,
                    Regado = i.WasIrrigationActivated ? "Sí" : "No"
                })
                .ToList(); 

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
