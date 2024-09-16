using Microsoft.EntityFrameworkCore;
using ComicsAPI.Models;
namespace ComicsAPI.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<ProductoDeseado> ProductosDeseados { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
  }
}
