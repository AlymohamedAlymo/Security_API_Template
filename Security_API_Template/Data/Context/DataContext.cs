using Microsoft.EntityFrameworkCore;
using Security_API_Template.Data.Entites;

namespace Security_API_Template.Data.Context
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AppUsers> Users { get; set; }

    }
}
