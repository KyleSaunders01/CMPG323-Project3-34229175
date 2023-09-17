using Models;

namespace EcoPower_Logistics.Repositories
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<Customer>> GetCustomersAsync();
        public Task<Customer> GetCustomerByIdAsync(int? id);
        public Task<Customer> AddCustomerAsync(Customer customer);
        public Task<Customer> UpdateCustomerAsync(Customer customer);
        public Task<Customer> DeleteCustomerAsync(int? id);
    }
}
