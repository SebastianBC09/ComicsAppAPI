using Microsoft.EntityFrameworkCore;
using ComicsAPI.Models;
namespace ComicsAPI.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options) {}
        public DbSet<Producto> Productos { get; set; }
    }
}
