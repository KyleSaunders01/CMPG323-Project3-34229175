using Models;

namespace EcoPower_Logistics.Repositories
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetOrdersAsync();
        public Task<Order> GetOrderByIdAsync(int? id);
        public Task<Order> AddOrderAsync(Order order);
        public Task<Order> UpdateOrderAsync(Order order);
        public Task<Order> DeleteOrderAsync(int? id);
    }
}
