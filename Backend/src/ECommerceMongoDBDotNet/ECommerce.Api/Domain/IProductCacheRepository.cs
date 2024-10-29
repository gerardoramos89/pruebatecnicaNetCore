using ECommerce.Api.ViewModels.ProductViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Infra.Repositories.CacheRepositories
{
    public interface IProductCacheRepository
    {
        Task<List<ProductViewModel>> GetAsync(); // Método para obtener todos los productos

        Task<ProductViewModel> GetAsync(string id); // Método para obtener un producto por ID

        Task<List<ProductViewModel>> GetProductByFilterAsync(string id, string productName, string productTitle); // Método para filtrar productos

        Task CreateAsync(ProductViewModel product); // Método para crear un producto

        Task CreateAsync(List<ProductViewModel> results); // Método para crear una lista de productos

        Task CreateManyAsync(IEnumerable<ProductViewModel> productViewModels); // Método para crear varios productos

        Task RemoveAsync(string id); // Método para eliminar un producto

        Task UpdateAsync(ProductViewModel product); // Método para actualizar un producto

    }
}
