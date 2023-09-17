using Models;

namespace EcoPower_Logistics.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProductsAsync();
        public Task<Product> GetProductByIdAsync(int? id);
        public Task<Product> AddProductAsync(Product product);
        public Task<Product> UpdateProductAsync(Product product);
        public Task<Product> DeleteProductAsync(int? id);
    }
}
