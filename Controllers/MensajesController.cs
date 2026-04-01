using Microsoft.AspNetCore.Mvc;
using SistemaCifradoToken.Models;
using SistemaCifradoToken.Services;
using System.Security.Cryptography;
using System.Text;

namespace SistemaCifradoToken.Controllers
{
    public class MensajesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CifradoService _cifradoService;

        public MensajesController(ApplicationDbContext context, CifradoService cifradoService)
        {
            _context = context;
            _cifradoService = cifradoService;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Mensaje mensaje)
        {
            bool usuarioExiste = _context.Usuarios.Any(u => u.IdUsuario == mensaje.IdUsuarioPropietario);

            if (!usuarioExiste)
            {
                ModelState.AddModelError("", "El usuario propietario no existe.");
            }

            if (string.IsNullOrWhiteSpace(mensaje.TextoCifrado))
            {
                ModelState.AddModelError("", "Debe ingresar un texto.");
            }

            if (!ModelState.IsValid)
            {
                return View(mensaje);
            }

            string textoOriginal = mensaje.TextoCifrado;
            mensaje.HashIntegridad = CalcularSha256(textoOriginal);
            mensaje.TextoCifrado = _cifradoService.Cifrar(textoOriginal);

            string token;
            do
            {
                token = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
            }
            while (_context.Mensajes.Any(m => m.Token == token));

            mensaje.Token = token;
            mensaje.FechaCreacion = DateTime.Now;
            mensaje.Estado = "Activo";
            mensaje.TotalIntentos = 0;
            mensaje.TotalExitosos = 0;

            _context.Mensajes.Add(mensaje);
            _context.SaveChanges();

            TempData["Mensaje"] = $"Mensaje guardado correctamente. Token generado: {mensaje.Token}";
            return RedirectToAction("Lista");
        }

        [HttpGet]
        public IActionResult Lista()
        {
            var mensajes = _context.Mensajes
                .OrderByDescending(m => m.IdMensaje)
                .ToList();

            return View(mensajes);
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var mensaje = _context.Mensajes.FirstOrDefault(m => m.IdMensaje == id);

            if (mensaje != null)
            {
                mensaje.Estado = "Eliminado";
                mensaje.FechaEliminacion = DateTime.Now;
                _context.SaveChanges();
            }

            return RedirectToAction("Lista");
        }

        [HttpGet]
        public IActionResult ConsultarToken()
        {
            return View(new ConsultaTokenViewModel());
        }

        [HttpPost]
        public IActionResult ConsultarToken(ConsultaTokenViewModel modelo)
        {
            if (modelo.IdUsuarioAccion <= 0)
            {
                modelo.MensajeError = "Debe ingresar el Id del usuario que consulta.";
                return View(modelo);
            }

            if (string.IsNullOrWhiteSpace(modelo.Token))
            {
                modelo.MensajeError = "Debe ingresar un token.";
                return View(modelo);
            }

            string tokenBuscado = modelo.Token.Trim().ToUpper();

            bool usuarioAccionExiste = _context.Usuarios.Any(u => u.IdUsuario == modelo.IdUsuarioAccion);

            if (!usuarioAccionExiste)
            {
                modelo.MensajeError = "El usuario que consulta no existe.";
                return View(modelo);
            }

            var mensaje = _context.Mensajes.FirstOrDefault(m => m.Token == tokenBuscado);

            if (mensaje == null)
            {
                _context.HistorialAccesos.Add(new HistorialAcceso
                {
                    IdMensaje = null,
                    IdUsuarioAccion = modelo.IdUsuarioAccion,
                    TokenIngresado = tokenBuscado,
                    Resultado = "Fallido",
                    Motivo = "El token no existe.",
                    FechaHora = DateTime.Now
                });

                _context.SaveChanges();

                modelo.MensajeError = "El token no existe.";
                return View(modelo);
            }

            if (mensaje.Estado == "Eliminado")
            {
                _context.HistorialAccesos.Add(new HistorialAcceso
                {
                    IdMensaje = mensaje.IdMensaje,
                    IdUsuarioAccion = modelo.IdUsuarioAccion,
                    TokenIngresado = tokenBuscado,
                    Resultado = "Fallido",
                    Motivo = "El mensaje fue eliminado logicamente.",
                    FechaHora = DateTime.Now
                });

                _context.SaveChanges();

                modelo.MensajeError = "El mensaje fue eliminado logicamente.";
                return View(modelo);
            }

            var usuarioPropietario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == mensaje.IdUsuarioPropietario);

            _context.HistorialAccesos.Add(new HistorialAcceso
            {
                IdMensaje = mensaje.IdMensaje,
                IdUsuarioAccion = modelo.IdUsuarioAccion,
                TokenIngresado = tokenBuscado,
                Resultado = "Exitoso",
                Motivo = "Consulta correcta del token.",
                FechaHora = DateTime.Now
            });

            mensaje.TotalIntentos += 1;
            mensaje.TotalExitosos += 1;

            if (mensaje.FechaPrimerUso == null)
            {
                mensaje.FechaPrimerUso = DateTime.Now;
            }

            _context.SaveChanges();

            string textoDescifrado;
            try
            {
                textoDescifrado = _cifradoService.Descifrar(mensaje.TextoCifrado ?? "");
            }
            catch
            {
                textoDescifrado = mensaje.TextoCifrado ?? "";
            }

            modelo.Token = tokenBuscado;
            modelo.TextoDescifrado = textoDescifrado;
            modelo.NombreUsuarioPropietario = usuarioPropietario?.NombreUsuario;
            modelo.FechaHoraIntento = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            return View(modelo);
        }

        private string CalcularSha256(string texto)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
            return Convert.ToHexString(bytes);
        }
    }
}