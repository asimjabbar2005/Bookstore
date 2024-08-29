using BusinessLayer.DTO;

namespace Bookstore.GraphQL.Types
{
    public class AuthorType : ObjectType<AuthorDto>
    {
        protected override void Configure(IObjectTypeDescriptor<AuthorDto> descriptor)
        {
            descriptor.Field(a => a.Id).Type<NonNullType<IdType>>();
            descriptor.Field(a => a.Name).Type<StringType>();
            descriptor.Field(a => a.Books).Type<ListType<BookType>>();
        }
    }
}
