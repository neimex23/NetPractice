using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPractice.Data;
using NetPractice.Models;

namespace NetPractice.Controllers
{
    public class ConfederacionController : Controller
    {

        public IActionResult Manage() => View(DataStore.Confederaciones);

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Confederacion c)
        {
            if (ModelState.IsValid)
            {
                DataStore.Confederaciones.Add(c);
                return RedirectToAction("Manage");
            }
            return View(c);
        }

        public IActionResult Edit(string Id)
        {
            var existente = DataStore.Confederaciones.FirstOrDefault(x => x.Id == Id);

            if (existente == null)
                return NotFound();

            return View(existente);
        }

        [HttpPost]
        public IActionResult Edit(Confederacion conf)
        {
            if (ModelState.IsValid)
            {
                var existente = DataStore.Confederaciones.FirstOrDefault(x => x.Id == conf.Id);

                if (existente == null)
                   return NotFound();

                existente.Nombre = conf.Nombre;

                return RedirectToAction("Manage");
            }

            return View(conf);
        }

        public IActionResult Delete(string Id)
        {
            var existente = DataStore.Confederaciones.FirstOrDefault(x => x.Id == Id);

            if (existente == null)
                return NotFound();

            DataStore.Confederaciones.Remove(existente);

            return RedirectToAction("Manage");
        }


        [HttpGet]
        public IActionResult Find(string search)
        {
            var confs = DataStore.Confederaciones;
            if (!string.IsNullOrEmpty(search))
            {
                confs = confs
                    .Where(p => p.Nombre.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (confs == null || !confs.Any())
            {
                ViewBag.Message = "No se encontraron confederaciones que coincidan con la búsqueda.";
                return View("Manage", new List<Confederacion>());
            }

            return View("Manage", confs);
        }
    }
}
