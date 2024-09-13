using Microsoft.EntityFrameworkCore;

namespace ComicsAPI.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {
        }
    }
}
