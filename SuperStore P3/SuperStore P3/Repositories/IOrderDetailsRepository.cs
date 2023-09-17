using Models;

namespace EcoPower_Logistics.Repositories
{
    public interface IOrderDetailsRepository
    {
        public Task<IEnumerable<OrderDetail>> GetOrderDetailsAsync();
        public Task<OrderDetail> GetOrderDetailByIdAsync(int? id);
        public Task<OrderDetail> AddOrderDetailAsync(OrderDetail orderDetail);
        public Task<OrderDetail> UpdateOrderDetailAsync(OrderDetail orderDetail);
        public Task<OrderDetail> DeleteOrderDetailAsync(int? id);
    }
}
