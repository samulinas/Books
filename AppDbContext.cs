using Microsoft.EntityFrameworkCore;
using Books.Models;
namespace Books
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        }
        public DbSet<CategoryBook> CategoryBook { get; set; }
        public DbSet<Book> Book { get; set; }
    }
}
