using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Api.Domain.Entitys
{
    public class Reservation
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public int ReservationId { get; private set; }
        public string CustomerId { get; private set; }
        public string ServiceId { get; private set; }
        public DateTime ReservationDate { get; private set; }
        public bool IsActive { get; private set; }

        // Constructor
        public Reservation(int reservationId, string customerId, string serviceId, DateTime reservationDate, bool isActive)
        {
            ReservationId = reservationId;
            CustomerId = customerId;
            ServiceId = serviceId;
            ReservationDate = reservationDate;
            IsActive = isActive;
        }
    }
}
