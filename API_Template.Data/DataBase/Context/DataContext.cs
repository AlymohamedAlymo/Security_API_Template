using Microsoft.EntityFrameworkCore;
using API_Template.Data.DataBase.Entites;

namespace API_Template.Data.DataBase.Context
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AppUsers> Users { get; set; }

    }
}
