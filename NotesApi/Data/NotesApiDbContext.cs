using Microsoft.EntityFrameworkCore;
using NotesApi.Model;

namespace NotesApi.Data
{
    public class NotesApiDbContext : DbContext
    {
        public NotesApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Notes> Notes { get; set; }
    }
}
