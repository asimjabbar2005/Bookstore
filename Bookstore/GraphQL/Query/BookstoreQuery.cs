using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.GraphQL.Query
{
    public class BookstoreQuery
    {
        private readonly BookstoreDbContext _bookstoreDbContext;
        public BookstoreQuery(BookstoreDbContext bookstoreDbContext)
        {
            _bookstoreDbContext = bookstoreDbContext;
        }

        public IQueryable<Book> Book => _bookstoreDbContext.Books.Include(b => b.Author);
        public IQueryable<Author> Authors => _bookstoreDbContext.Authors;
    }
}
