using InventoryApi.Data;
using InventoryApi.Entities;
using InventoryApi.Interface;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;
        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }
        public async Task<Product> AddAsync(Product p)
        {
            var productEntry = await _dbContext.Products.AddAsync(p);
            await _dbContext.SaveChangesAsync();
            return productEntry.Entity;
        }
        public async Task UpdateAsync(Product p)
        {
            _dbContext.Products.Update(p);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
