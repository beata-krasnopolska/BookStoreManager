﻿using BookStoreManager.Models;
using BookStoreManager.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookStoreManager.Controllers
{
    [Route("api/bookStore/{bookStoreId}/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public ActionResult CreateBook([FromRoute] int bookStoreId, [FromBody] CreateBookDto createBookDto)
        {
            var bookId = _bookService.CreateBook(bookStoreId, createBookDto);

            return Created($"api/bookStore/{bookStoreId}/book/{bookId}", null);
        }

        [HttpGet("{dishId}")]
        public ActionResult<BookDto> GetBook([FromRoute] int bookStoreId, [FromRoute] int bookId)
        {
           var result = _bookService.GetBook(bookStoreId, bookId);

            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<BookDto>> GetAllBooks([FromRoute] int bookStoreId)
        {
            var result = _bookService.GetAllBooks(bookStoreId);

            return Ok(result);
        }
    }
}
