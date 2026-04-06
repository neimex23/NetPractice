using Microsoft.AspNetCore.Http;
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

        public IActionResult Manage() => View(_depRepo.GetAll());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Deporte d)
        {
            if (ModelState.IsValid)
            {
                _depRepo.Add(d);
                return RedirectToAction("Manage");
            }
            return View(d);
        }

        public IActionResult Find(string search)
        {
            var deportes = _depRepo.Search(search);
            if (deportes == null || !deportes.Any())
            {
                ViewBag.Message = "No se encontraron deportes que coincidan con la búsqueda.";
                return View("Manage", new List<Deporte>());
            }

            return View("Manage", deportes);
        }


        public IActionResult Edit(string Id)
        {
            var existente = _depRepo.GetById(Id);

            if (existente == null)
                return NotFound();

            return View(existente);
        }

        [HttpPost]
        public IActionResult Edit(Deporte depo)
        {
            if (ModelState.IsValid)
            {
                _depRepo.Update(depo);

                return RedirectToAction("Manage");
            }

            return View(depo);
        }

        public IActionResult Delete(string Id)
        {
            _depRepo.Delete(Id);

            return RedirectToAction("Manage");
        }
    }
}
