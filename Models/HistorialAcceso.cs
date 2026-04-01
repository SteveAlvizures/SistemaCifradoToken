using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCifradoToken.Models
{
    [Table("HistorialAccesos")]
    public class HistorialAcceso
    {
        [Key]
        public int IdHistorial { get; set; }

        public int? IdMensaje { get; set; }

        public int IdUsuarioAccion { get; set; }

        public string TokenIngresado { get; set; } = string.Empty;

        public string Resultado { get; set; } = string.Empty;

        public string? Motivo { get; set; }

        public string? DireccionIP { get; set; }

        public string? UserAgent { get; set; }

        public string? Dispositivo { get; set; }

        public DateTime FechaHora { get; set; }
    }
}