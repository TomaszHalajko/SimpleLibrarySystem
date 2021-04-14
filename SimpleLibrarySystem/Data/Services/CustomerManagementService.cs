using Microsoft.EntityFrameworkCore;
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

        public event Func<string, Task> Notify;

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

        public Task<bool> RemoveCustomerAsync(Customer currentCustomer)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                //need to tell context that state is unchanged 
                context.Entry(currentCustomer).State = EntityState.Unchanged;

                context.Customers.Remove(currentCustomer);
                context.SaveChanges();

                if (Notify != null)
                    Notify.Invoke(String.Empty);
            }
            return Task.FromResult(true);
        }

        public Task<bool> AddCustomerAsync(Customer currentCustomer)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                string lastId = "";
                List<Customer> customers = context.Customers.ToList();
                if (customers.Count() > 0)
                {
                    Customer last = customers.Last();
                    lastId = last.CustomerID;
                    int id = int.Parse(lastId) + 1;
                    lastId = "";
                    string id_s = id.ToString();
                    for (int i = id_s.Length; i < 7; i++)
                    {
                        lastId += "0";
                    }
                    lastId += id_s;
                }
                else
                {
                    lastId = "0000001";
                }
                currentCustomer.CustomerID = lastId;

                //borrowing book only for one month
                context.Customers.Add(currentCustomer);
                context.SaveChanges();

                if (Notify != null)
                    Notify.Invoke(String.Empty);
            }
            return Task.FromResult(true);
        }
    }
}
