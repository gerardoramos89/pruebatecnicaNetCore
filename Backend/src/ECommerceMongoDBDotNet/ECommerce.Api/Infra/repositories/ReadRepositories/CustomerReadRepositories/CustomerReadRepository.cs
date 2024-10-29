using ECommerce.Api.Domain;
using ECommerce.Api.Domain.Entitys;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Infra.Repositories.ReadRepositories.CustomerReadRepositories
{
    public class CustomerReadRepository : ICustomerReadRepository
    {
        public async Task<Customer> GetCustomerAsync(int customerId)
        {
            return await Task.Run(() =>
                       new Customer(customerId, "inputModel.CustomerName", "inputModel.CustomerEmail", "inputModel.CustomerPhone", true));

        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await Task.Run(async () => Enumerable.Empty<Customer>());
        }

        public async Task<bool> IsExistCustomerAsync(int customerId)
        {
            return await Task.Run(() => false);
        }
    }
}
