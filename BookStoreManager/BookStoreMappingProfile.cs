using AutoMapper;
using BookStoreManager.Entities;
using BookStoreManager.Models;

namespace BookStoreManager
{
    public class BookStoreMappingProfile : Profile
    {
        public BookStoreMappingProfile()
        {
            CreateMap<BookStore, BookStoreDto>()
                .ForMember(b => b.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(b => b.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(b => b.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Book, BookDto>().MaxDepth(3);
        }
    }
}
