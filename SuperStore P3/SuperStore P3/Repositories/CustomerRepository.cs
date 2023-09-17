using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EcoPower_Logistics.Repositories
{
    public class CustomerRepository :ICustomerRepository
    {
        private readonly SuperStoreContext _context;

        public CustomerRepository(SuperStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var result = await _context.Customers.ToListAsync();

            return result;
        }

        public async Task<Customer> GetCustomerByIdAsync(int? id)
        {
            if (CheckCustomerAsync(id) != null)
            {
                var result = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);

                return result;
            }
            else
            {
                throw new ArgumentException("The Customer doesn't exist in the system");
            }
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            if (CheckCustomerAsync(customer.CustomerId) != null)
            {
                var result = await _context.AddAsync(customer);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new ArgumentException("The Customer can't be added to the system");
            }
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            if(CheckCustomerAsync(customer.CustomerId) == null)
            {
                var result = _context.Update(customer);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new ArgumentException("The Customer doesn't exist in the system");
            }
        }

        public async Task<Customer> DeleteCustomerAsync(int? id)
        {
            if(CheckCustomerAsync(id) != null)
            {
                var existingCustomer = await CheckCustomerAsync(id);
                var result = _context.Customers.Remove(existingCustomer);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            else
            {
                throw new ArgumentException("The Customer doesn't exist in the system");
            }
        }

        private async Task<Customer> CheckCustomerAsync(int? id)
        {
            var findCustomer = await _context.Customers.FindAsync(id);
            if(findCustomer != null)
            {
                return findCustomer;
            }
            else
            {
                return null;
            }
        }
    }
}
