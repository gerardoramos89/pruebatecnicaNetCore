using ECommerce.Api.Domain;
using ECommerce.Api.Domain.Entitys;
using ECommerce.Api.Helpers;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Infra.Repositories.CacheRepositories.MongoCacheRepositories
{
    public class CustomerCacheRepository : ICustomerCacheRepository
    {
        #region Fields

        private readonly IMongoCollection<Customer> _customersCollection;
        private readonly FilterDefinitionBuilder<Customer> _filter;

        #endregion Fields

        #region Ctor

        public CustomerCacheRepository(IOptions<CustomerDatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _customersCollection = mongoDatabase.GetCollection<Customer>(settings.Value.CustomerCollectionName);
            _filter = Builders<Customer>.Filter;
        }

        #endregion Ctor

        #region Implement

        public async Task CreateAsync(Customer customer)
            => await _customersCollection.InsertOneAsync(customer);

        public async Task CreateManyAsync(IEnumerable<Customer> customers)
            => await _customersCollection.InsertManyAsync(customers);

        public async Task<List<Customer>> GetAsync()
            => await _customersCollection.Find(_ => true).ToListAsync();

        public async Task<Customer?> GetAsync(int id)
            => await _customersCollection.Find(x => x.CustomerId == id).FirstOrDefaultAsync();
        public async Task<Customer?> GetCustomerAsync(string id)
{
    // Supongamos que id es un string que se convierte a int
    if (int.TryParse(id, out int customerId))
    {
        return await _customersCollection.Find(c => c.CustomerId == customerId).FirstOrDefaultAsync();
    }
    return null; // O manejar el caso donde la conversión falle
}
        public async Task<List<Customer>> GetCustomersByFilterAsync(string id, string name, string email)
        {
            var filter = _filter.Empty; // Inicializar un filtro vacío

            if (!string.IsNullOrEmpty(id))
            {
                filter &= _filter.Eq(c => c.CustomerId.ToString(), id);
            }

            if (!string.IsNullOrEmpty(name))
            {
                filter &= _filter.Eq(c => c.CustomerName, name);
            }

            if (!string.IsNullOrEmpty(email))
            {
                filter &= _filter.Eq(c => c.CustomerEmail, email);
            }

            return await _customersCollection.Find(filter).ToListAsync();
        }

        public async Task RemoveAsync(int id)
            => await _customersCollection.FindOneAndDeleteAsync(c => c.CustomerId == id);

        public async Task<bool> UpdateAsync(Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.CustomerId, customer.CustomerId);

            // Crear una actualización solo con los campos que deseas modificar
            var update = Builders<Customer>.Update
                .Set(c => c.CustomerName, customer.CustomerName)
                .Set(c => c.CustomerEmail, customer.CustomerEmail)
                .Set(c => c.CustomerPhone, customer.CustomerPhone)
                .Set(c => c.IsActive, customer.IsActive);

            // Ejecutar la actualización
            var result = await _customersCollection.UpdateOneAsync(filter, update);

            // Devuelve true si se modificó un documento, de lo contrario false
            return result.ModifiedCount > 0;
        }
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customersCollection.Find(_ => true).ToListAsync();
        }

        #endregion Implement
    }
}
