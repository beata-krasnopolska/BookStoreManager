﻿using AutoMapper;
using BookStoreManager.Entities;
using BookStoreManager.Exceptions;
using BookStoreManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreManager.Services
{
    public class BookStoreService: IBookStoreService
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<BookStoreService> _logger;

        public BookStoreService(BookStoreDbContext dbContext, IMapper mapper, ILogger<BookStoreService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public IEnumerable<BookStoreDto> GetAllBookStores(BookStoreQuery query)
        {
            var bookStores = _dbContext.BookStores
                .Include(b => b.Books)
                .Include(a => a.Address)
                .Where(x => query.SearchParam == null || (x.Name.ToLower().Contains(query.SearchParam.ToLower()) || x.Description.ToLower().Contains(query.SearchParam.ToLower())))
                .Skip(query.PageNumber * (query.PageNumber -1))
                .Take(query.PageSize)
                .ToList();

            if (bookStores == null)
            {
                throw new Exception();
            }

            var bookStoresDto = _mapper.Map<List<BookStoreDto>>(bookStores);

            return bookStoresDto;
        }

        public BookStoreDto GetBookStoreById(int id)
        {
            var bookStore = _dbContext.BookStores
                .Include(b => b.Books)
                .Include(a => a.Address)
                .FirstOrDefault(b => b.Id == id);

            if (bookStore is null)
            {
                throw new ItemNotFoundException("BookStore not found");
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

        public void DeleteBookStore(int id)
        {
            
            var bookStore = _dbContext.BookStores.FirstOrDefault(x=>x.Id == id);
            if (bookStore is null)
            {
                _logger.LogError($"Delete invoked BookStore NOT FOUND {id}");
                throw new ItemNotFoundException("BookStore not found"); ;
            }

            _dbContext.BookStores.Remove(bookStore);
            _dbContext.SaveChanges();
        }

        public void UpdateBookStore(UpdateBookStoreDto dto, int id)
        {
            var bookStore = _dbContext.BookStores
                .Include(_ => _.Books)
                .Include(_ => _.Address)
                .FirstOrDefault(x=>x.Id == id);
            if (bookStore is null)
                throw new ItemNotFoundException("BookStore not found");

            _mapper.Map(dto, bookStore);

            _dbContext.SaveChanges();
        }
    }
}
