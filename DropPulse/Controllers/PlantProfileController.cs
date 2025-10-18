using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DropPulse.Controllers
{
    public class PlantProfileController : Controller
    {
        // GET: PlantProfileController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PlantProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlantProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlantProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlantProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlantProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
