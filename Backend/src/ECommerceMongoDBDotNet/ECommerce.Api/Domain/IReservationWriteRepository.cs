using ECommerce.Api.Domain.Entitys;
using System.Threading.Tasks;

namespace ECommerce.Api.Domain
{
    public interface IReservationWriteRepository
    {
        Task CreateAsync(Reservation reservation);
        Task<bool> UpdateAsync(Reservation reservation);
        Task RemoveAsync(int id);
    }
}
