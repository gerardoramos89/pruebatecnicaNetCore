using ECommerce.Api.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Infra.Repositories.ReadRepositories.CustomerReadRepositories
{
    public interface ICustomerReadRepository
    {
        Task<Customer> GetCustomerAsync(int customerId);
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<bool> IsExistCustomerAsync(int customerId);
    }
}
