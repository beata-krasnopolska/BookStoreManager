using BookStoreManager.Models;
using System.Collections.Generic;

namespace BookStoreManager.Services
{
    public interface IBookStoreService
    {
        IEnumerable<BookStoreDto> GetAllBookStores();

        BookStoreDto GetBookStoreById(int id);

        int CreateBookStore(CretaeBookStoreDto cretaeBookStoreDto);

        void DeleteBookStore(int id);

        void UpdateBookStore(UpdateBookStoreDto dto, int id);
    }
}
