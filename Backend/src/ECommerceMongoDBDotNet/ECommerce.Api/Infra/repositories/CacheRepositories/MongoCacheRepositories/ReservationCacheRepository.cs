using ECommerce.Api.Domain.Entitys; // Asegúrate de que la ruta de la entidad Reservation sea correcta
using ECommerce.Api.Helpers;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Infra.Repositories.CacheRepositories
{
    public class ReservationCacheRepository : IReservationCacheRepository // Asegúrate de tener esta interfaz definida
    {
        #region Fields

        private readonly IMongoCollection<Reservation> _reservationsCollection; // Colección de reservas
        private readonly FilterDefinitionBuilder<Reservation> _filter; // Constructor de filtros para reservas

        #endregion Fields

        #region Ctor

        public ReservationCacheRepository(IOptions<ReservationDatabaseSettings> settings) // Asegúrate de tener ReservationDatabaseSettings configurado
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _reservationsCollection = mongoDatabase.GetCollection<Reservation>(settings.Value.ReservationCollectionName);
            _filter = Builders<Reservation>.Filter; // Inicializar el constructor de filtros
        }

        #endregion Ctor

        #region Implement

        public async Task CreateAsync(Reservation reservation) // Crear una nueva reserva
            => await _reservationsCollection.InsertOneAsync(reservation);

        public async Task CreateManyAsync(IEnumerable<Reservation> reservations) // Crear múltiples reservas
            => await _reservationsCollection.InsertManyAsync(reservations);

        public async Task<List<Reservation>> GetAsync() // Obtener todas las reservas
            => await _reservationsCollection.Find(_ => true).ToListAsync();

        public async Task<Reservation?> GetAsync(int id) // Obtener una reserva por ID
            => await _reservationsCollection.Find(x => x.ReservationId == id).FirstOrDefaultAsync();

        public async Task RemoveAsync(int id) // Eliminar una reserva por ID
            => await _reservationsCollection.FindOneAndDeleteAsync(r => r.ReservationId == id);

        public async Task<bool> UpdateAsync(Reservation reservation) // Actualizar una reserva
        {
            var filter = Builders<Reservation>.Filter.Eq(r => r.Id, reservation.Id);
            var result = await _reservationsCollection.ReplaceOneAsync(filter, reservation);
            return result.IsAcknowledged && result.ModifiedCount > 0; // Retorna si la operación fue exitosa
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByFilterAsync(string serviceId, string customerId, DateTime? date) // Obtener reservas filtradas
        {
            var filter = _filter.Empty; // Inicializar un filtro vacío

            if (!string.IsNullOrEmpty(serviceId))
            {
                filter &= _filter.Eq(r => r.ServiceId, serviceId); // Filtro por ID de servicio
            }

            if (!string.IsNullOrEmpty(customerId))
            {
                filter &= _filter.Eq(r => r.CustomerId, customerId); // Filtro por ID de cliente
            }

            if (date.HasValue)
            {
                filter &= _filter.Eq(r => r.ReservationDate.Date, date.Value.Date); // Filtro por fecha de reserva
            }

            return await _reservationsCollection.Find(filter).ToListAsync(); // Retorna las reservas que cumplen el filtro
        }

        #endregion Implement
    }
}

