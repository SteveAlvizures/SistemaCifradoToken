using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaCifradoToken.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string NombreUsuario { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Estado { get; set; } = "Activo";

        public DateTime FechaCreacion { get; set; }
    }
}