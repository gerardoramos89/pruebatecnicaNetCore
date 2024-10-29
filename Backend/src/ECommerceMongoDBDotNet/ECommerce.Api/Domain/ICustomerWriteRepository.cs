using ECommerce.Api.Domain.Entitys;
using System.Threading.Tasks;

namespace ECommerce.Api.Domain
{
    public interface ICustomerWriteRepository
    {
        Task<int> CreateCustomerAsync(Customer customer);
        Task UpdateCustomeAsync(Customer customer); // Asegúrate de que el nombre sea correcto.
        Task DeleteCustomerAsync(int customerId);
    }
}
