using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EcoPower_Logistics.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SuperStoreContext _context;

        public OrderRepository(SuperStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var result = await _context.Orders.ToListAsync();
            return result;
        }

        public async Task<Order> GetOrderByIdAsync(int? id)
        {
            if(CheckOrderAsync(id) != null)
            {
                var result = await _context.Orders.FirstOrDefaultAsync(c => c.OrderId == id);
                return result;
            }
            else
            {
                throw new ArgumentException("The Order doesn't exist in the system");
            }
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            if(CheckOrderAsync(order.OrderId) == null)
            {
                var result = await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
            { 
                throw new ArgumentException("The Order can't be added to the system"); 
            }
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            if(CheckOrderAsync(order.OrderId) != null)
            {
                var result = _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new ArgumentException("The Order doesn't exist in the system");
            }
        }

        public async Task<Order> DeleteOrderAsync(int? id)
        {
            if(CheckOrderAsync(id) != null)
            {
                var existingOrder = await CheckOrderAsync(id);
                var result = _context.Orders.Remove(existingOrder);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new ArgumentException("The Order doesn't exist in the system");
            }
        }

        private async Task<Order> CheckOrderAsync(int? id)
        {
            var findOrder = await _context.Orders.FindAsync(id);
            if(findOrder != null)
            {
                return findOrder;
            }
            else
            {
                return null;
            }
        }
    }
}
