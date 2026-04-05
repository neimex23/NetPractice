using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPractice.Data;
using NetPractice.Models;

namespace NetPractice.Controllers
{
    public class ConfederacionController : Controller
    {
        private readonly IConfederacionRepository _confRepo;

        public ConfederacionController(IConfederacionRepository confRepo)
        {
            _confRepo = confRepo;
        }

        public IActionResult Manage() => View(_confRepo.GetAll()); //View(DataStore.Confederaciones);

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Confederacion c)
        {
            if (ModelState.IsValid)
            {
                //DataStore.Confederaciones.Add(c);
                _confRepo.Add(c);

                return RedirectToAction("Manage");
            }
            return View(c);
        }

        public IActionResult Edit(string Id)
        {
            //var existente = DataStore.Confederaciones.FirstOrDefault(x => x.Id == Id);

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
                //var existente = DataStore.Confederaciones.FirstOrDefault(x => x.Id == conf.Id);

                //if (existente == null)
                   //return NotFound();

                //existente.Nombre = conf.Nombre;

                _confRepo.Update(conf);

                return RedirectToAction("Manage");
            }

            return View(conf);
        }

        public IActionResult Delete(string Id)
        {
            /*var existente = DataStore.Confederaciones.FirstOrDefault(x => x.Id == Id);

            if (existente == null)
                return NotFound();

            DataStore.Confederaciones.Remove(existente);*/

            _confRepo.Delete(Id);

            return RedirectToAction("Manage");
        }


        [HttpGet]
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
