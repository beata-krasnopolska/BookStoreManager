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
                .ForMember(b => b.City, c => c.MapFrom(s => s.Address.City)).MaxDepth(3)
                .ForMember(b => b.Street, c => c.MapFrom(s => s.Address.Street)).MaxDepth(3)
                .ForMember(b => b.PostalCode, c => c.MapFrom(s => s.Address.PostalCode)).MaxDepth(3);

            CreateMap<Book, BookDto>().MaxDepth(3);

            CreateMap<CretaeBookStoreDto, BookStore>()
                .ForMember(b => b.Address, r => r.MapFrom(dto => new Address()
                { City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode }));

            CreateMap<UpdateBookStoreDto, BookStore>();

            CreateMap<CreateBookDto, Book>();
        }
    }
}
