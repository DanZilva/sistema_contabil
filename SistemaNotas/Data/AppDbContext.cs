using Microsoft.EntityFrameworkCore;
using SistemaNotas.Models;

namespace SistemaNotas.Data
{
    
    public class AppDbContext : DbContext
    {
        public DbSet<NotaFiscal> Notas { get; set;}
        public DbSet<Usuario> Usuarios {get; set;}

        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            
        }

    }

}