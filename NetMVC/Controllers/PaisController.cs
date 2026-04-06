using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPracticeCore.Data;
using NetPracticeCore.Models;

namespace NetPracticeMVC.Controllers
{
    public class PaisController : Controller
    {
        private readonly IPaisRepository _paisRepo;
        private readonly IConfederacionRepository _confRepo;
        private readonly IDeporteRepository _depRepo;

        public PaisController(
            IPaisRepository paisRepo,
            IConfederacionRepository confRepo,
            IDeporteRepository depRepo)
        {
            _paisRepo = paisRepo;
            _confRepo = confRepo;
            _depRepo = depRepo;
        }

        public IActionResult Manage()
        {
            //return View(DataStore.Paises);
            return View(_paisRepo.GetAll());
        }

        public IActionResult Find(string search)
        {
            var paises = _paisRepo.Search(search);

            if (paises == null || !paises.Any())
            {
                ViewBag.Message = "No se encontraron países que coincidan con la búsqueda.";
                return View("Manage", new List<Pais>());
            }

            return View("Manage", paises);
        }

        public IActionResult Create()
        {
            ViewBag.Confederaciones = _confRepo.GetAll();
            ViewBag.Deportes = _depRepo.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Pais model)
        {
            _paisRepo.Add(model);

            return RedirectToAction("Manage");
        }

        public IActionResult Edit(string Id)
        {
            var existente = _paisRepo.GetById(Id);

            if (existente == null)
                return NotFound();

            ViewBag.Confederaciones = _confRepo.GetAll();
            ViewBag.Deportes = _depRepo.GetAll();

            return View(existente);
        }
        [HttpPost]
        public IActionResult Edit(string id,Pais pais)
        {
            var existente = _paisRepo.GetById(id);

            if (existente == null)
                return NotFound();

            existente.Nombre = pais.Nombre;
            existente.FechaFundacion = pais.FechaFundacion;

            existente.ConfederacionId = pais.ConfederacionId;
            existente.DeporteId = pais.DeporteId;

            existente.Confederacion = _confRepo.GetById(pais.ConfederacionId);
            existente.Deporte = _depRepo.GetById(pais.DeporteId);

            _paisRepo.Update(existente);

            return RedirectToAction("Manage");
        }

        public IActionResult Delete(string Id)
        {
            _paisRepo.Delete(Id);

            return RedirectToAction("Manage");
        }
    }
}
