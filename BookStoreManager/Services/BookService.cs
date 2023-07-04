﻿using AutoMapper;
using BookStoreManager.Entities;
using BookStoreManager.Exceptions;
using BookStoreManager.Models;
using System.Linq;

namespace BookStoreManager.Services
{
    public class BookService : IBookService
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookService(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public int CreateBook(int bookStoreId, CreateBookDto createBookDto) 
        {
            var bookStore = _dbContext.BookStores.FirstOrDefault(x => x.Id == bookStoreId);
            if (bookStore == null)
                throw new ItemNotFoundException("The book store not found");

            var bookEntity = _mapper.Map<Book>(createBookDto);

            bookEntity.BookstoreId = bookStoreId;

            _dbContext.Books.Add(bookEntity);
            _dbContext.SaveChanges();
            return bookEntity.Id;
        }

        public BookDto GetBook(int bookStoreId, int bookId)
        {
            var bookStore = _dbContext.BookStores.FirstOrDefault(x=>x.Id == bookStoreId);
            if (bookStore == null)
                throw new ItemNotFoundException("The restaurant not found");

            var book = _dbContext.Books.FirstOrDefault(x => x.Id == bookId);

            if (book == null || book.BookstoreId != bookStoreId)
                throw new ItemNotFoundException("Book not found");


            return _mapper.Map<BookDto>(book);
        }
    }
}
