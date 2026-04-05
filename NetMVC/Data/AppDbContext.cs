using Microsoft.EntityFrameworkCore;
using NetPractice.Models;

namespace NetPractice.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Confederacion> Confederaciones { get; set; }
        public DbSet<Deporte> Deportes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
