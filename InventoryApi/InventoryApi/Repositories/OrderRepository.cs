using InventoryApi.Data;
using InventoryApi.Entities;
using InventoryApi.Interface;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .AsNoTracking()
                .ToListAsync();
        }
        public Task<Order?> GetByIdAsync(int id)
        {
            return _context.Orders.Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }
        public async Task<Order> AddAsync(Order o)
        {
            // Calculate TotalAmount based on OrderItems
            if (o.Items != null && o.Items.Any())
            {
                o.TotalAmount = o.Items.Sum(item => item.Quantity * item.UnitPrice);
            }
            else
            {
                o.TotalAmount = 0;
            }

            // Reduce stock for each OrderItem
            if (o.Items != null)
            {
                foreach (var item in o.Items)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null)
                    {
                        throw new InvalidOperationException($"Product with ID {item.ProductId} not found.");
                    }
                    if (product.StockQuantity < item.Quantity)
                    {
                        throw new InvalidOperationException($"Insufficient stock for product ID {item.ProductId}.");
                    }
                    product.StockQuantity -= item.Quantity;
                }
            }
            _context.Orders.Add(o);
            await _context.SaveChangesAsync();
            return o;
        }

    }
}
