using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Models
{
    public class BookstoreDbContext : DbContext
    {      
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options)
        : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);

            base.OnModelCreating(modelBuilder);

            //// Seed Authors
            //modelBuilder.Entity<Author>().HasData(
            //    new Author { Id = 1, Name = "Author 1" },
            //    new Author { Id = 2, Name = "Author 2" }
            //);

            //// Seed Books
            //modelBuilder.Entity<Book>().HasData(
            //    new Book { Id = 1, Title = "Book 1", ISBN = "1234567890", AuthorId = 1 },
            //    new Book { Id = 2, Title = "Book 2", ISBN = "0987654321", AuthorId = 2 }
            //);
        }
    }

}
