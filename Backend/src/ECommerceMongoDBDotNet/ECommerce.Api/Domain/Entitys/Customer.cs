using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace ECommerce.Api.Domain.Entitys
{
    public class Customer
    {
        [BsonId] // Atributo que indica que esta propiedad es el ID de MongoDB
        public ObjectId Id { get; set; } // Usando ObjectId para MongoDB

        public int CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerEmail { get; private set; }
        public string CustomerPhone { get; private set; }
        public bool IsActive { get; private set; }

        // Constructor sin parámetros
        public Customer() { }

        [JsonConstructor]
        public Customer(int customerId, string customerName, string customerEmail, string customerPhone, bool isActive)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            CustomerPhone = customerPhone;
            IsActive = isActive;
        }
    }
}

