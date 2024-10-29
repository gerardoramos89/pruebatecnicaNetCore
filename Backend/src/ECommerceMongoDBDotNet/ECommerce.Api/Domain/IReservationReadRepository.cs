using ECommerce.Api.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Infra.Repositories.ReadRepositories.CustomerReadRepositories
{
    public interface IReservationReadRepository
    {
        Task<List<Reservation>> GetAsync();
        Task<Reservation?> GetAsync(int id);
        Task<List<Reservation>> GetByCustomerIdAsync(int customerId); // Obtener reservas por cliente
    }
}
