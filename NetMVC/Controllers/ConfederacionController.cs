using Microsoft.AspNetCore.Mvc;
using NetPracticeCore.Data;
using NetPracticeCore.Models;

namespace NetPracticeMVC.Controllers
{
    public class ConfederacionController : Controller
    {
        private readonly IConfederacionRepository _confRepo;

        public ConfederacionController(IConfederacionRepository confRepo)
        {
            _confRepo = confRepo;
        }

        public IActionResult Manage() => View(_confRepo.GetAll()); 

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Confederacion c)
        {
            if (ModelState.IsValid)
            {
                _confRepo.Add(c);

                return RedirectToAction("Manage");
            }
            return View(c);
        }

        public IActionResult Edit(string Id)
        {
            var existente = _confRepo.GetById(Id); 

            if (existente == null)
                return NotFound();

            return View(existente);
        }

        [HttpPost]
        public IActionResult Edit(Confederacion conf)
        {
            if (ModelState.IsValid)
            {
                _confRepo.Update(conf);

                return RedirectToAction("Manage");
            }

            return View(conf);
        }

        public IActionResult Delete(string Id)
        {
            _confRepo.Delete(Id);

            return RedirectToAction("Manage");
        }


        [HttpGet]
        public IActionResult Find(string search)
        {
            var confs = _confRepo.Search(search);
            if (confs == null || !confs.Any())
            {
                ViewBag.Message = "No se encontraron confederaciones que coincidan con la búsqueda.";
                return View("Manage", new List<Confederacion>());
            }

            return View("Manage", confs);
        }
    }
}
