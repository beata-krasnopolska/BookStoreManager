using AutoMapper;
using BookStoreManager.Entities;
using BookStoreManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreManager.Services
{
    public class BookStoreService: IBookStoreService
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public BookStoreService(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<BookStoreDto> GetAllBookStores()
        {
            var bookStores = _dbContext.BookStores.Include(b => b.Books).Include(a => a.Address).ToList();

            if (bookStores == null)
            {
                throw new Exception();
            }

            var bookStoresDto = _mapper.Map<List<BookStoreDto>>(bookStores);

            return bookStoresDto;
        }

        public BookStoreDto GetBookStoreById(int id)
        {
            var bookStore = _dbContext.BookStores.Include(b => b.Books).Include(a => a.Address).FirstOrDefault(b => b.Id == id);

            if (bookStore is null)
            {
                throw new Exception();
            }

            var bookStoreDto = _mapper.Map<BookStoreDto>(bookStore);

            return bookStoreDto;
        }

        public int CreateBookStore(CretaeBookStoreDto cretaeBookStoreDto)
        {
            var bookStore = _mapper.Map<BookStore>(cretaeBookStoreDto);

            _dbContext.BookStores.Add(bookStore);
            _dbContext.SaveChanges();

            return bookStore.Id;
        }

        public bool DeleteBookStore(int id)
        {
            var bookStore = _dbContext.BookStores.FirstOrDefault(x=>x.Id == id);
            if (bookStore is null) return false;

            _dbContext.BookStores.Remove(bookStore);
            _dbContext.SaveChanges();

            return true;
        }

        public bool UpdateBookStore(UpdateBookStoreDto dto, int id)
        {
            var bookStore = _dbContext.BookStores
                .Include(_ => _.Books)
                .Include(_ => _.Address)
                .FirstOrDefault(x=>x.Id == id);
            if (bookStore is null) return false;

            _mapper.Map(dto, bookStore);

            _dbContext.SaveChanges();

            return true;
        }
    }
}
