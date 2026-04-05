using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPractice.Data;
using NetPractice.Models;

namespace NetPractice.Controllers
{
    public class DeporteController : Controller
    {
        public IActionResult Manage() => View(DataStore.Deportes);

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Deporte d)
        {
            if (ModelState.IsValid)
            {
                DataStore.Deportes.Add(d);
                return RedirectToAction("Manage");
            }
            return View(d);
        }

        public IActionResult Find(string search)
        {
            var deportes = DataStore.Deportes;
            if (!string.IsNullOrEmpty(search))
            {
                deportes = deportes
                    .Where(p => p.Nombre.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (deportes == null || !deportes.Any())
            {
                ViewBag.Message = "No se encontraron deportes que coincidan con la búsqueda.";
                return View("Manage", new List<Deporte>());
            }

            return View("Manage", deportes);
        }


        public IActionResult Edit(string Id)
        {
            var existente = DataStore.Deportes.FirstOrDefault(x => x.Id == Id);

            if (existente == null)
                return NotFound();

            return View(existente);
        }

        [HttpPost]
        public IActionResult Edit(Deporte depo)
        {
            if (ModelState.IsValid)
            {
                var existente = DataStore.Deportes.FirstOrDefault(x => x.Id == depo.Id);

                if (existente == null)
                    return NotFound();

                existente.Nombre = depo.Nombre;

                return RedirectToAction("Manage");
            }

            return View(depo);
        }

        public IActionResult Delete(string Id)
        {
            var existente = DataStore.Deportes.FirstOrDefault(x => x.Id == Id);

            if (existente == null)
                return NotFound();

            DataStore.Deportes.Remove(existente);

            return RedirectToAction("Manage");
        }
    }
}
