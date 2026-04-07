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

        public async Task<IActionResult> Manage()
        {
            var confs = await _confRepo.GetAll();
            return View(confs);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Confederacion c)
        {
            if (ModelState.IsValid)
            {
                await _confRepo.Add(c);
                return RedirectToAction("Manage");
            }
            return View(c);
        }

        public async Task<IActionResult> Edit(string Id)
        {
            var existente = await _confRepo.GetById(Id);

            if (existente == null)
                return NotFound();

            return View(existente);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Confederacion conf)
        {
            if (ModelState.IsValid)
            {
                await _confRepo.Update(conf);
                return RedirectToAction("Manage");
            }

            return View(conf);
        }

        public async Task<IActionResult> Delete(string Id)
        {
            await _confRepo.Delete(Id);
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public async Task<IActionResult> Find(string search)
        {
            var confs = await _confRepo.Search(search);

            if (confs == null || !confs.Any())
            {
                ViewBag.Message = "No se encontraron confederaciones que coincidan con la búsqueda.";
                return View("Manage", new List<Confederacion>());
            }

            return View("Manage", confs);
        }
    }
}