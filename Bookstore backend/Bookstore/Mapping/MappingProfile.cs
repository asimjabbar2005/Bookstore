using AutoMapper;
using BusinessLayer.DTO;
using DataLayer.Models;

namespace Bookstore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}
