using ECommerce.Api.Domain;
using ECommerce.Api.Domain.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Infra.Repositories.WriteRepositories.CustomerWriteRepositories
{
    public class CustomerWriteRepository : ICustomerWriteRepository
    {
        // Aquí podrías tener una referencia a la base de datos o contexto de MongoDB.
        // private readonly IMongoCollection<Customer> _customersCollection;

        public CustomerWriteRepository(/* Puedes inyectar el contexto de la base de datos aquí */)
        {
            // Inicializa la colección de clientes aquí si es necesario.
            // _customersCollection = // Obtener la colección de clientes desde el contexto.
        }

        public async Task<int> CreateCustomerAsync(Customer customer)
        {
            // Lógica para insertar el cliente en la base de datos.
            await Task.Delay(1000); // Simulación de un retraso en la operación.
            return Random.Shared.Next(); // Devuelve un ID de cliente simulado.
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            // Lógica para eliminar el cliente de la base de datos.
            await Task.Delay(1000); // Simulación de un retraso en la operación.
        }

        public async Task UpdateCustomeAsync(Customer customer) // Asegúrate de que el nombre y la firma coincidan
        {
            // Aquí deberías agregar la lógica para actualizar el cliente en la base de datos.
            await Task.Delay(1000); // Simulación de un retraso en la operación.
            // Por ejemplo, actualizar en la base de datos.
            // await _customersCollection.ReplaceOneAsync(c => c.CustomerId == customer.CustomerId, customer);
        }
    }
}
