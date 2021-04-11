using Microsoft.EntityFrameworkCore;
using SimpleLibrarySystem.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLibrarySystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Borrowings>()
                .HasKey(c => new { c.CustomerID, c.BookID });

            builder.Entity<Borrowings>()
                .HasOne<Customer>(cp => cp.Customer)
                .WithMany(s => s.Borrowings)
                .HasForeignKey(cp => cp.CustomerID);

            builder.Entity<Borrowings>()
                .HasOne<Book>(cp => cp.Book)
                .WithMany(s => s.Borrowings)
                .HasForeignKey(cp => cp.BookID);

            builder.Entity<Book>().HasData(
                new Book { BookID = "2013-000001", Name="Szklany tron", Author="Sarah J. Mass", Publisher="Uroboros", ReleaseDate = 2013, ISBN= "9788377478844" },
                new Book { BookID = "2012-000001", Name = "Drużyna Pierścienia", Author = "J.R.R. Tolkien", Publisher = "Amber", ReleaseDate = 2012, ISBN= "9788324144242" },
                new Book { BookID = "2014-000001", Name = "Ostatnie życzenie", Author = "Andrzej Sapkowski", Publisher = "superNowa", ReleaseDate = 2014, ISBN= "9788375780635" }
                );

            builder.Entity<Customer>().HasData(
                new Customer { CustomerID = "0000001", Vorname="Tomasz", Name="Hałajko", PhoneNumber="883791001", Email="halajko.tomasz@gmail.com", Address="Jelenia Góra, ul.Kadetów 1" }
                );
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Borrowings> Borrowings { get; set; }
    }
}
