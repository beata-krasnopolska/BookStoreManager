using BookStoreManager.Models;

namespace BookStoreManager.Services
{
    public interface IBookService
    {
        int CreateBook(int bookStoreId, CreateBookDto createBookDto);


    }
}
