using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EcoPower_Logistics.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly SuperStoreContext _context;
        
        public OrderDetailsRepository(SuperStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsAsync()
        {
            var result = await _context.OrderDetails.ToListAsync();
            return result;
        }
        public async Task<OrderDetail> GetOrderDetailByIdAsync(int? id)
        {
            if(CheckOrderDetailAsync(id) != null)
            {
                var result = await _context.OrderDetails.FirstOrDefaultAsync(c => c.OrderDetailsId == id);
                return result;
            }
            else
            {
                throw new ArgumentException("The OrderDetail doesn't exist in the system");
            }
        }
        public async Task<OrderDetail> AddOrderDetailAsync(OrderDetail orderDetail)
        {
            if(CheckOrderDetailAsync(orderDetail.OrderDetailsId) == null)
            {
                var result = await _context.OrderDetails.AddAsync(orderDetail);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new ArgumentException("The OrderDetail can't be added to the system");
            }
        }
        public async Task<OrderDetail> UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            if(CheckOrderDetailAsync(orderDetail.OrderDetailsId) != null )
            {
                var result = _context.OrderDetails.Update(orderDetail);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new ArgumentException("The OrderDetail doesn't exist in the system");
            }
        }
        public async Task<OrderDetail> DeleteOrderDetailAsync(int? id)
        {
            var existingOrderDetail = await CheckOrderDetailAsync(id);
            if (existingOrderDetail != null)
            {
                var result = _context.OrderDetails.Remove(existingOrderDetail);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new ArgumentException("The OrderDetail doesn't exist in the system");
            }
        }
        private async Task<OrderDetail> CheckOrderDetailAsync(int? id)
        {
            var findOrderDetail = await _context.OrderDetails.FindAsync(id);
            if(findOrderDetail != null)
            {
                return findOrderDetail;
            }
            else
            {
                return null;
            }
        }
    }
}
