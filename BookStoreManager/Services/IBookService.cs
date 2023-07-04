using BookStoreManager.Models;
using System.Collections.Generic;

namespace BookStoreManager.Services
{
    public interface IBookService
    {
        int CreateBook(int bookStoreId, CreateBookDto createBookDto);

        BookDto GetBook(int bookStoreId, int bookId);

        List<BookDto> GetAllBooks(int bookStoreId);
    }
}
