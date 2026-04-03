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

        [HttpPost]
        public IActionResult Create(Pais model)
        {
            model.Confederacion = DataStore.Confederaciones.FirstOrDefault(x => x.Id == model.ConfederacionId);
            model.Deporte = DataStore.Deportes.FirstOrDefault(x => x.Id == model.DeporteId);

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
        public IActionResult Edit(string id,Pais pais)
        {
            /*// === DEPURACIÓN ===
            System.Diagnostics.Debug.WriteLine("=== INFORMACIÓN DE DEPURACIÓN ===");
            System.Diagnostics.Debug.WriteLine($"ID del modelo recibido: '{pais.Id}'");
            System.Diagnostics.Debug.WriteLine($"Nombre recibido: '{pais.Nombre}'");

            // Ver todos los datos que llegan del formulario
            foreach (var key in Request.Form.Keys)
            {
                System.Diagnostics.Debug.WriteLine($"Form[{key}] = '{Request.Form[key]}'");
            }

            // Ver los IDs disponibles en la base de datos
            System.Diagnostics.Debug.WriteLine("IDs en DataStore:");
            foreach (var p in DataStore.Paises)
            {
                System.Diagnostics.Debug.WriteLine($"  - '{p.Id}'");
            }
            // === FIN DEPURACIÓN ===*/


            var existente = DataStore.Paises.FirstOrDefault(x => x.Id == id);

            if (existente == null)
                return NotFound();

            existente.Nombre = pais.Nombre;
            existente.FechaFundacion = pais.FechaFundacion;

            existente.ConfederacionId = pais.ConfederacionId;
            existente.DeporteId = pais.DeporteId;

            existente.Confederacion = DataStore.Confederaciones
                .FirstOrDefault(x => x.Id == pais.ConfederacionId);

            existente.Deporte = DataStore.Deportes
                .FirstOrDefault(x => x.Id == pais.DeporteId);

            return RedirectToAction("Manage");
        }
    }
}
