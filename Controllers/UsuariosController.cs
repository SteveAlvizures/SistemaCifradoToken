using Microsoft.AspNetCore.Mvc;
using SistemaCifradoToken.Models;
using System.Security.Cryptography;
using System.Text;

namespace SistemaCifradoToken.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(Usuario usuario)
        {
            if (_context.Usuarios.Any(u => u.Correo == usuario.Correo))
            {
                ModelState.AddModelError("", "El correo ya existe.");
            }

            if (_context.Usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario))
            {
                ModelState.AddModelError("", "El nombre de usuario ya existe.");
            }

            if (ModelState.IsValid)
            {
                usuario.PasswordHash = CalcularSha256(usuario.PasswordHash);
                usuario.FechaCreacion = DateTime.Now;
                usuario.Estado = "Activo";

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                TempData["Mensaje"] = "Usuario registrado correctamente.";
                return RedirectToAction("Index", "Home");
            }

            return View(usuario);
        }

        private string CalcularSha256(string texto)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
            return Convert.ToHexString(bytes);
        }
    }
}