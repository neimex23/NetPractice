using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPractice.Data;
using NetPractice.Models;

namespace NetPractice.Controllers
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
            //var paises = DataStore.Paises;
            /*if (!string.IsNullOrEmpty(search))
            {
                paises = paises
                    .Where(p => p.Nombre.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }*/

            //return View(paises);

            var paises = _paisRepo.Search(search);

            if (paises == null || !paises.Any())
            {
                ViewBag.Message = "No se encontraron países que coincidan con la búsqueda.";
                return View("Manage", new List<Pais>());
            }

            return View("Manage", paises);
        }

        // GET: Pais/Create
        public IActionResult Create()
        {
            //ViewBag.Confederaciones = DataStore.Confederaciones;
            //ViewBag.Deportes = DataStore.Deportes;

            ViewBag.Confederaciones = _confRepo.GetAll();
            ViewBag.Deportes = _depRepo.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Pais model)
        {
            //model.Confederacion = DataStore.Confederaciones.FirstOrDefault(x => x.Id == model.ConfederacionId);
            //model.Deporte = DataStore.Deportes.FirstOrDefault(x => x.Id == model.DeporteId);
            //DataStore.Paises.Add(model);
            _paisRepo.Add(model);

            return RedirectToAction("Manage");
        }

        public IActionResult Edit(string Id)
        {
            //var existente = DataStore.Paises.FirstOrDefault(x => x.Id == Id);
            //ViewBag.Confederaciones = DataStore.Confederaciones;
            //ViewBag.Deportes = DataStore.Deportes;

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
            //var existente = DataStore.Paises.FirstOrDefault(x => x.Id == id);
            var existente = _paisRepo.GetById(id);

            if (existente == null)
                return NotFound();

            existente.Nombre = pais.Nombre;
            existente.FechaFundacion = pais.FechaFundacion;

            existente.ConfederacionId = pais.ConfederacionId;
            existente.DeporteId = pais.DeporteId;

            existente.Confederacion = _confRepo.GetById(pais.ConfederacionId);
            existente.Deporte = _depRepo.GetById(pais.DeporteId);

            //existente.Confederacion = DataStore.Confederaciones
            //.FirstOrDefault(x => x.Id == pais.ConfederacionId);

            //existente.Deporte = DataStore.Deportes
                //.FirstOrDefault(x => x.Id == pais.DeporteId);

            _paisRepo.Update(existente);

            return RedirectToAction("Manage");
        }

        public IActionResult Delete(string Id)
        {
            //var existente = DataStore.Paises.FirstOrDefault(x => x.Id == Id)
            //if (existente == null)
                //return NotFound();

            //DataStore.Paises.Remove(existente);
            _paisRepo.Delete(Id);

            return RedirectToAction("Manage");
        }
    }
}
