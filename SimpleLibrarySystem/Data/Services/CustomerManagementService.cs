using Microsoft.Extensions.DependencyInjection;
using SimpleLibrarySystem.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLibrarySystem.Data.Services
{
    public class CustomerManagementService
    {
        IServiceScopeFactory _serviceScopeFactory;

        public CustomerManagementService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task<List<Customer>> GetCustomersListAsync()
        {
            List<Customer> customers = new List<Customer>();
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                customers = context.Customers.ToList();
            }
            return Task.FromResult(customers);
        }
    }
}
