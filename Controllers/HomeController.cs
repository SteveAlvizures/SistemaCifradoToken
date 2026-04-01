using Microsoft.AspNetCore.Mvc;
using SistemaCifradoToken.Models;
using System.Diagnostics;

namespace SistemaCifradoToken.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalUsuarios = _context.Usuarios.Count();
            ViewBag.TotalMensajes = _context.Mensajes.Count();
            ViewBag.TotalHistorial = _context.HistorialAccesos.Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}