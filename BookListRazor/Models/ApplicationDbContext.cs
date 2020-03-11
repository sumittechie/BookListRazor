using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Book> Book { get; set; }
    }
}
