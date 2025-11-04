using Microsoft.EntityFrameworkCore;

namespace DigiStore.WebService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets will be added as entities are introduced.
    }
}