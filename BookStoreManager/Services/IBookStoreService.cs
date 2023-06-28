using BookStoreManager.Models;
using System.Collections.Generic;

namespace BookStoreManager.Services
{
    public interface IBookStoreService
    {
        public IEnumerable<BookStoreDto> GetAllBookStores();

        public BookStoreDto GetBookStoreById(int id);

        public int CreateBookStore(CretaeBookStoreDto cretaeBookStoreDto);

        public bool DeleteBookStore(int id);
    }
}
