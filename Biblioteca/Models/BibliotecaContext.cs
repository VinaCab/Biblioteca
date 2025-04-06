using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;  


namespace SistemaBiblioteca.Models
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
            : base(options)
        {
        }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Estado> Estado { get; set; }
    }
}
