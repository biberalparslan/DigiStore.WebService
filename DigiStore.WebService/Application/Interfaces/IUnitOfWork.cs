using System.Threading.Tasks;

namespace DigiStore.WebService.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}