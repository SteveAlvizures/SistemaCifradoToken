using Microsoft.EntityFrameworkCore;

namespace SistemaCifradoToken.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
        public DbSet<HistorialAcceso> HistorialAccesos { get; set; }
    }
}