using BusinessLayer.DTO;

namespace Bookstore.GraphQL.Mutation
{
    public class UpdateBookInput
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int AuthorId { get; set; }
    }
}
