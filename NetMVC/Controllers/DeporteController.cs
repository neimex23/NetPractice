using Microsoft.AspNetCore.Mvc;
using NetPracticeCore.Data;
using NetPracticeCore.Models;

namespace NetPracticeMVC.Controllers
{
    public class DeporteController : Controller
    {
        private readonly IDeporteRepository _depRepo;

        public DeporteController(IDeporteRepository depRepo)
        {
            _depRepo = depRepo;
        }

        public async Task<IActionResult> Manage()
        {
            var deportes = await _depRepo.GetAll();
            return View(deportes);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Deporte d)
        {
            if (ModelState.IsValid)
            {
                await _depRepo.Add(d);
                return RedirectToAction("Manage");
            }
            return View(d);
        }

        public async Task<IActionResult> Find(string search)
        {
            var deportes = await _depRepo.Search(search);

            if (deportes == null || !deportes.Any())
            {
                ViewBag.Message = "No se encontraron deportes que coincidan con la búsqueda.";
                return View("Manage", new List<Deporte>());
            }

            return View("Manage", deportes);
        }

        public async Task<IActionResult> Edit(string Id)
        {
            var existente = await _depRepo.GetById(Id);

            if (existente == null)
                return NotFound();

            return View(existente);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Deporte depo)
        {
            if (ModelState.IsValid)
            {
                await _depRepo.Update(depo);
                return RedirectToAction("Manage");
            }

            return View(depo);
        }

        public async Task<IActionResult> Delete(string Id)
        {
            await _depRepo.Delete(Id);
            return RedirectToAction("Manage");
        }
    }
}