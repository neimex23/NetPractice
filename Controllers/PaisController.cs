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

        // GET: Pais
        public IActionResult Find(string search)
        {
            var paises = DataStore.Paises;

            if (!string.IsNullOrEmpty(search))
            {
                paises = paises
                    .Where(p => p.Nombre.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(paises);
        }

        // GET: Pais/Create
        public IActionResult Create()
        {
            ViewBag.Confederaciones = DataStore.Confederaciones;
            ViewBag.Deportes = DataStore.Deportes;

            return View();
        }

        // POST: Pais/Create
        [HttpPost]
        public IActionResult Create(Pais pais)
        {
            if (!ModelState.IsValid)
                return View(pais);

            if (DataStore.Paises.Any(p => p.Nombre == pais.Nombre))
            {
                ModelState.AddModelError("", "El país ya existe");
                return View(pais);
            }

            Confederacion confederacion = null;// DataStore.Confederaciones.FirstOrDefault(c => c.Id == pais.Id);
            Deporte deporte = DataStore.Deportes.FirstOrDefault(d => d.Id == pais.DeporteId);

            if (confederacion == null || deporte == null)
            {
                ModelState.AddModelError("", "Confederacion o deporte no Existen");
                return View(pais);
            }
                

            pais.setID(DataStore.Paises.Count + 1);

            pais.Confederacion = confederacion;

            pais.Deporte = deporte;

            DataStore.Paises.Add(pais);

            return RedirectToAction(nameof(Index));
        }

        // GET: Pais/Edit/5
        public IActionResult Edit(int id)
        {
            var pais = DataStore.Paises.FirstOrDefault(p => p.Id == id);
            if (pais == null) return NotFound();

            ViewBag.Confederaciones = DataStore.Confederaciones;
            ViewBag.Deportes = DataStore.Deportes;

            return View(pais);
        }

        // POST: Pais/Edit
        [HttpPost]
        public IActionResult Edit(Pais pais)
        {
            var existing = DataStore.Paises.FirstOrDefault(p => p.Id == pais.Id);
            if (existing == null) return NotFound();

            existing.Nombre = pais.Nombre;
            existing.FechaFundacion = pais.FechaFundacion;
            existing.ConfederacionId = pais.ConfederacionId;
            existing.DeporteId = pais.DeporteId;

            //existing.Confederacion = DataStore.Confederaciones
                //.FirstOrDefault(c => c.Id == pais.ConfederacionId);

            existing.Deporte = DataStore.Deportes
                .FirstOrDefault(d => d.Id == pais.DeporteId);

            return RedirectToAction(nameof(Index));
        }

        // GET: Pais/Delete/5
        public IActionResult Delete(int id)
        {
            var pais = DataStore.Paises.FirstOrDefault(p => p.Id == id);
            if (pais == null) return NotFound();

            return View(pais);
        }

        // POST: Pais/Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var pais = DataStore.Paises.FirstOrDefault(p => p.Id == id);
            if (pais != null)
            {
                DataStore.Paises.Remove(pais);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Pais/Details/5
        public IActionResult Details(int id)
        {
            var pais = DataStore.Paises.FirstOrDefault(p => p.Id == id);
            if (pais == null) return NotFound();

            return View(pais);
        }
    }
}
