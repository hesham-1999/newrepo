using Microsoft.EntityFrameworkCore;

namespace api1.Model
{
    public class appcontext : DbContext
    {
        public appcontext(DbContextOptions<appcontext> options) : base(options) { }
        public DbSet<Country> countries { get; set; }

    }
}
