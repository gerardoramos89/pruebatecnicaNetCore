using ECommerce.Api.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Domain
{
    public interface ICustomerCacheRepository
    {
        Task CreateAsync(Customer customer);
        Task CreateManyAsync(IEnumerable<Customer> customers);
        Task<List<Customer>> GetAsync();
        Task<Customer?> GetAsync(int id);
        Task<Customer?> GetCustomerAsync(string id); // Asegúrate de que esté presente
        Task RemoveAsync(int id);
        Task<bool> UpdateAsync(Customer customer); // Cambiado a Task<bool>
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
