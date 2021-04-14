using Microsoft.EntityFrameworkCore;
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

        public event Func<string, Task> Notify;

        public Task<List<Book>> GetBooksListAsync()
        {
            List<Book> books = new List<Book>();
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                books = context.Books.Include("Borrowings").Include("Borrowings.Customer").ToList();
            }
            return Task.FromResult(books);
        }

        public Task<bool> SaveNewBorrowingAsync(Customer currentCustomer, Book currentBook)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                //need to tell context that state is unchanged 
                context.Entry(currentCustomer).State = EntityState.Unchanged;
                context.Entry(currentBook).State = EntityState.Unchanged;

                //borrowing book only for one month
                context.Borrowings.Add(new Borrowings { Customer = currentCustomer, Book = currentBook, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(1) });
                context.SaveChanges();

                if (Notify != null)
                    Notify.Invoke(String.Empty);
            }
            return Task.FromResult(true);
        }

        public Task<bool> ReturnBookAsync(Borrowings borrowing)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                //need to tell context that state is unchanged 
                context.Entry(borrowing).State = EntityState.Unchanged;

                context.Borrowings.Remove(borrowing);
                context.SaveChanges();

                if (Notify != null)
                    Notify.Invoke(String.Empty);
            }
            return Task.FromResult(true);
        }

        public Task<bool> RemoveBookAsync(Book currentBook)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                //need to tell context that state is unchanged 
                context.Entry(currentBook).State = EntityState.Unchanged;

                context.Books.Remove(currentBook);
                context.SaveChanges();

                if (Notify != null)
                    Notify.Invoke(String.Empty);
            }
            return Task.FromResult(true);
        }

        public Task<bool> AddBookAsync(Book currentBook)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                string lastId = "";
                List<Book> booksSameYear = context.Books.Where(e => e.ReleaseDate == currentBook.ReleaseDate).ToList();
                if (booksSameYear.Count() > 0)
                {
                    Book last = booksSameYear.Last();
                    lastId = last.BookID;
                    int id = int.Parse(lastId.Split('-').Last()) + 1;
                    string id_s = id.ToString();
                    lastId = currentBook.ReleaseDate.ToString() + "-";
                    for(int i = id_s.Length; i < 6; i++)
                    {
                        lastId += "0";
                    }
                    lastId += id_s;
                }
                else
                {
                    lastId = currentBook.ReleaseDate.ToString() + "-000001";
                }
                currentBook.BookID = lastId;

                //borrowing book only for one month
                context.Books.Add(currentBook);
                context.SaveChanges();

                if (Notify != null)
                    Notify.Invoke(String.Empty);
            }
            return Task.FromResult(true);
        }
    }
}
