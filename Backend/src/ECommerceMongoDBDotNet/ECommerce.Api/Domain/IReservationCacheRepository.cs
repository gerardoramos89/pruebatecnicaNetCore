using ECommerce.Api.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Infra.Repositories.CacheRepositories
{
    public interface IReservationCacheRepository
    {
        Task CreateAsync(Reservation reservation);
        Task<List<Reservation>> GetAsync();
        Task<Reservation?> GetAsync(int id);
        Task<bool> UpdateAsync(Reservation reservation);
        Task RemoveAsync(int id);
    }
}
