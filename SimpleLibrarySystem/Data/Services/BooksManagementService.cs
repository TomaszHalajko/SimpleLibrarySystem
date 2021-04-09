using Microsoft.Extensions.DependencyInjection;
using SimpleLibrarySystem.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLibrarySystem.Data.Services
{
    public class BooksManagementService
    {
        IServiceScopeFactory _serviceScopeFactory;

        public BooksManagementService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task<List<Book>> GetBooksListAsync()
        {
            List<Book> books = new List<Book>();
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                books = context.Books.ToList();
            }
            return Task.FromResult(books);
        }
    }
}
