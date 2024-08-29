namespace Bookstore_frontend.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
    }

    public class GraphQLBookData
    {
        public List<Book> Book { get; set; }
    }

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GraphQLAuthorsData
    {
        public List<Author> Authors { get; set; }
    }
}
