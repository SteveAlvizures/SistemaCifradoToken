using Microsoft.AspNetCore.Mvc;
using SistemaCifradoToken.Models;

namespace SistemaCifradoToken.Controllers
{
    public class HistorialController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistorialController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Lista(int? idUsuarioPropietario)
        {
            var historial = _context.HistorialAccesos.AsQueryable();

            if (idUsuarioPropietario.HasValue)
            {
                var idsMensajesDelUsuario = _context.Mensajes
                    .Where(m => m.IdUsuarioPropietario == idUsuarioPropietario.Value)
                    .Select(m => m.IdMensaje)
                    .ToList();

                historial = historial.Where(h =>
                    h.IdMensaje.HasValue &&
                    idsMensajesDelUsuario.Contains(h.IdMensaje.Value));
            }

            var resultado = historial
                .OrderByDescending(h => h.IdHistorial)
                .ToList();

            return View(resultado);
        }
    }
}