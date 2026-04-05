using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPractice.Data;
using NetPractice.Models;

namespace NetPractice.Controllers
{
    public class PaisController : Controller
    {
        public IActionResult Manage()
        {
           return View(DataStore.Paises);
        }

        public IActionResult Find(string search)
        {
            var paises = DataStore.Paises;
            if (!string.IsNullOrEmpty(search))
            {
                paises = paises
                    .Where(p => p.Nombre.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (paises == null || !paises.Any())
            {
                ViewBag.Message = "No se encontraron países que coincidan con la búsqueda.";
                return View("Manage", new List<Pais>());
            }

            return View("Manage", paises);
        }

        public IActionResult Create()
        {
            ViewBag.Confederaciones = DataStore.Confederaciones;
            ViewBag.Deportes = DataStore.Deportes;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Pais model)
        {
            var Confederacion = DataStore.Confederaciones.FirstOrDefault(x => x.Id == model.ConfederacionId);
            var Deporte = DataStore.Deportes.FirstOrDefault(x => x.Id == model.DeporteId);
    
            if (Confederacion == null || Deporte == null)
            {
                ModelState.AddModelError("", "Confederación o Deporte no válidos.");
                return NotFound();
            }

            model.Confederacion = Confederacion;
            model.Deporte = Deporte;
            DataStore.Paises.Add(model);

            return RedirectToAction("Manage");
        }

        public IActionResult Edit(string Id)
        {
            var existente = DataStore.Paises.FirstOrDefault(x => x.Id == Id);

            if (existente == null)
                return NotFound();

            ViewBag.Confederaciones = DataStore.Confederaciones;
            ViewBag.Deportes = DataStore.Deportes;

            return View(existente);
        }
        [HttpPost]
        public IActionResult Edit(string id, Pais pais)
        {
            var existente = DataStore.Paises.FirstOrDefault(x => x.Id == id);
            Confederacion? confederacion = DataStore.Confederaciones.FirstOrDefault(x => x.Id == pais.ConfederacionId);
            Deporte? deporte = DataStore.Deportes.FirstOrDefault(x => x.Id == pais.DeporteId);

            if (existente == null || confederacion == null || deporte == null)
                return NotFound();

            existente.Nombre = pais.Nombre;
            existente.FechaFundacion = pais.FechaFundacion;

            existente.ConfederacionId = pais.ConfederacionId;
            existente.DeporteId = pais.DeporteId;

            existente.Confederacion = confederacion;
            existente.Deporte = deporte;


            return RedirectToAction("Manage");
        }

        public IActionResult Delete(string Id)
        {
            var existente = DataStore.Paises.FirstOrDefault(x => x.Id == Id);
            if (existente == null)
                return NotFound();

            DataStore.Paises.Remove(existente);
            return RedirectToAction("Manage");
        }
    }
}
