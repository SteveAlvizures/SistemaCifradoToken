using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCifradoToken.Models
{
    [Table("Mensajes")]
    public class Mensaje
    {
        [Key]
        public int IdMensaje { get; set; }

        public int IdUsuarioPropietario { get; set; }

        public string TextoCifrado { get; set; } = string.Empty;

        public string? HashIntegridad { get; set; }

        public string? Token { get; set; }

        public string? Etiqueta { get; set; }

        public string? Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaPrimerUso { get; set; }

        public DateTime? FechaEliminacion { get; set; }

        public int TotalIntentos { get; set; }

        public int TotalExitosos { get; set; }
    }
}