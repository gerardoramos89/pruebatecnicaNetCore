using ECommerce.Api.Domain; // Asegúrate de que este namespace es correcto
using ECommerce.Api.Domain.Entitys;
using ECommerce.Api.Helpers;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Infra.Repositories.WriteRepositories
{
    public class ReservationWriteRepository : IReservationWriteRepository
    {
        private readonly IMongoCollection<Reservation> _reservationsCollection;

        public ReservationWriteRepository(IOptions<ReservationDatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _reservationsCollection = mongoDatabase.GetCollection<Reservation>(settings.Value.ReservationCollectionName);
        }

        public async Task CreateAsync(Reservation reservation)
        {
            await _reservationsCollection.InsertOneAsync(reservation);
        }

        public async Task<bool> UpdateAsync(Reservation reservation)
        {
            var result = await _reservationsCollection.ReplaceOneAsync(r => r.Id == reservation.Id, reservation);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<Reservation?> GetAsync(int id)
        {
            return await _reservationsCollection.Find(r => r.ReservationId == id).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(int id)
        {
            await _reservationsCollection.DeleteOneAsync(r => r.ReservationId == id);
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _reservationsCollection.Find(_ => true).ToListAsync();
        }
    }
}
