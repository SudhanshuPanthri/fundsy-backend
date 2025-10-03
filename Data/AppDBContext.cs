using Fundsy_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Fundsy_backend.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
