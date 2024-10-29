using ECommerce.Api.ViewModels.ProductViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Infra.Repositories.CacheRepositories
{
    public class ProductCacheRepository : IProductCacheRepository
    {
        // Simulación de almacenamiento, reemplázalo con tu lógica real
        private readonly List<ProductViewModel> _products = new();

        public Task<List<ProductViewModel>> GetAsync()
        {
            return Task.FromResult(_products);
        }

        public Task<ProductViewModel> GetAsync(string id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public Task<List<ProductViewModel>> GetProductByFilterAsync(string id, string productName, string productTitle)
        {
            var filteredProducts = _products
                .Where(p => p.ProductName.Contains(productName) || p.ProductTitle.Contains(productTitle))
                .ToList();
            return Task.FromResult(filteredProducts);
        }

        public Task CreateAsync(ProductViewModel product)
        {
            _products.Add(product);
            return Task.CompletedTask;
        }

        public Task CreateAsync(List<ProductViewModel> results)
        {
            _products.AddRange(results);
            return Task.CompletedTask;
        }

        public Task CreateManyAsync(IEnumerable<ProductViewModel> productViewModels)
        {
            _products.AddRange(productViewModels);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
            return Task.CompletedTask;
        }

        public Task UpdateAsync(ProductViewModel product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                // Actualiza las propiedades del producto existente
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductTitle = product.ProductTitle;
                // Actualiza otras propiedades según sea necesario
            }
            return Task.CompletedTask;
        }
    }
}
