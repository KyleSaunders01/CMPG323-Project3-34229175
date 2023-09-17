using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EcoPower_Logistics.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SuperStoreContext _context;

        public ProductRepository(SuperStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var result = await _context.Products.ToListAsync();
            return result;
        }

        public async Task<Product> GetProductByIdAsync(int? id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(c => c.ProductId == id);
            return result;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            if(CheckProductAsync(product.ProductId) == null)
            {
                var result = await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new ArgumentException("The Product can't be added to the system");
            }
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            if(CheckProductAsync(product.ProductId) != null)
            {
                var result = _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new ArgumentException("The Product doesn't exist in the system");
            }
        }

        public async Task<Product> DeleteProductAsync(int? id)
        {
            var existingProduct = await CheckProductAsync(id);
            if(existingProduct != null)
            {
                var result = _context.Products.Remove(existingProduct);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new ArgumentException("The Product doesn't exist in the system");
            }
        }
        private async Task<Product> CheckProductAsync(int? id)
        {
            var findProduct = await _context.Products.FindAsync(id);
            if (findProduct != null)
            {
                return findProduct;
            }
            else
            {
                return null;
            }
        }
    }
}
