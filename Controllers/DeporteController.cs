using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPractice.Data;
using NetPractice.Models;

namespace NetPractice.Controllers
{
    public class DeporteController : Controller
    {
        public IActionResult Index()
        {
            return View(DataStore.Deportes);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Deporte d)
        {
            d.setID(DataStore.Deportes.Count + 1);
            DataStore.Deportes.Add(d);
            return RedirectToAction(nameof(Index));
        }
    }
}
