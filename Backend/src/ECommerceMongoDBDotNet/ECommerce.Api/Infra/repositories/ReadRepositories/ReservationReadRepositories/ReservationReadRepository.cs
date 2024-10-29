using ECommerce.Api.Domain.Entitys;
using ECommerce.Api.Helpers;
using ECommerce.Api.Infra.Repositories.ReadRepositories.CustomerReadRepositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Infra.Repositories.ReadRepositories
{
    public class ReservationReadRepository : IReservationReadRepository
    {
        private readonly IMongoCollection<Reservation> _reservationsCollection;

        public ReservationReadRepository(IOptions<ReservationDatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _reservationsCollection = mongoDatabase.GetCollection<Reservation>(settings.Value.ReservationCollectionName);
        }

        public async Task<List<Reservation>> GetAsync()
        {
            return await _reservationsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Reservation?> GetAsync(int id)
        {
            return await _reservationsCollection.Find(x => x.ReservationId == id).FirstOrDefaultAsync();
        }

        public async Task<List<Reservation>> GetByCustomerIdAsync(int customerId)
        {
            return await _reservationsCollection.Find(r => r.ReservationId == customerId).ToListAsync();
        }
    }
}
