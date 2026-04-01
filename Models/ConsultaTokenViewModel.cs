using System.ComponentModel.DataAnnotations;

namespace SistemaCifradoToken.Models
{
    public class ConsultaTokenViewModel
    {
        [Required]
        public int IdUsuarioAccion { get; set; }

        [Required]
        public string Token { get; set; } = string.Empty;

        public string? TextoDescifrado { get; set; }

        public string? NombreUsuarioPropietario { get; set; }

        public string? FechaHoraIntento { get; set; }

        public string? MensajeError { get; set; }
    }
}