using BusinessLayer.DTO;

namespace Bookstore.GraphQL.Mutation
{
    public class CreateBookInput
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int AuthorId { get; set; }
    }
}
