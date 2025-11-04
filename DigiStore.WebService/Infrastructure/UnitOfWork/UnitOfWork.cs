using System.Threading.Tasks;
using DigiStore.WebService.Application.Interfaces;
using DigiStore.WebService.Infrastructure.Data;

namespace DigiStore.WebService.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}