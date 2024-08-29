using AutoMapper;
using BusinessLayer.DTO;
using DataLayer.Models;

namespace Bookstore.GraphQL.Mutation
{
    public class BooksMutation
    {
        private readonly BookstoreDbContext _bookstoreDbContext;
        private readonly IMapper _mapper;

        public BooksMutation(BookstoreDbContext bookstoreDbContext,
                             IMapper mapper)
        {
            _bookstoreDbContext = bookstoreDbContext;
            _mapper = mapper;
        }

        public Book CreateBook(CreateBookInput createBookInput)
        {
            var author = _bookstoreDbContext.Authors.Find(createBookInput.AuthorId);

            var book = new Book()
            {
                Title = createBookInput.Title,
                ISBN = createBookInput.ISBN,
                Author = author
            };

            _bookstoreDbContext.Add(book);
            _bookstoreDbContext.SaveChanges();

            return book;
        }
        
        public Book UpdateBook(UpdateBookInput updateBookInput)
        {
            var dbBook = _bookstoreDbContext.Books.Find(updateBookInput.Id);
            if (dbBook != null)
            {
                var author = _bookstoreDbContext.Authors.Find(updateBookInput.AuthorId);

                dbBook.Title = updateBookInput.Title;
                dbBook.ISBN = updateBookInput.ISBN;
                dbBook.Author = author;

                _bookstoreDbContext.Update(dbBook);
                _bookstoreDbContext.SaveChanges();
            }

            return dbBook;
        }

        public Book DeleteBook(DeleteBookInput deleteBookInput)
        {
            var book = new Book()
            {
                Id = deleteBookInput.Id
            };

            _bookstoreDbContext.Remove(book);
            _bookstoreDbContext.SaveChanges();

            return book;
        }
    }
}
