using BusinessLayer.DTO;

namespace Bookstore.GraphQL.Types
{
    public class BookType : ObjectType<BookDto>
    {
        protected override void Configure(IObjectTypeDescriptor<BookDto> descriptor)
        {
            descriptor.Field(x => x.Id).Type<IdType>();
            descriptor.Field(x => x.Title).Type<StringType>();
            descriptor.Field(x => x.ISBN).Type<StringType>();
            descriptor.Field(x => x.AuthorId).Type<IdType>();
            descriptor.Field(x => x.Author).Type<AuthorType>();
        }
    }
}
