using AutoMapper;
using BookStoreManager.Entities;
using BookStoreManager.Models;
using BookStoreManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreManager.Controllers
{
    [Route("api/bookStore")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly IBookStoreService _bookStoreService;

        public BookStoreController(IBookStoreService bookStoreService)
        {
            _bookStoreService = bookStoreService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookStoreDto>> GetAllBookStores()
        {
            var bookStores = _bookStoreService.GetAllBookStores();

            if(bookStores == null)
            {
                return BadRequest();
            }

            return Ok(bookStores);
        }

        [HttpGet("{id}")]
        public ActionResult<BookStoreDto> GetBookStoresById([FromRoute] int id)
        {
            var bookStore = _bookStoreService.GetBookStoreById(id);

            if(bookStore is null)
            {
                return NotFound();
            }

            return Ok(bookStore);
        }

        [HttpPost]
        public ActionResult<int> CreateBookStore([FromBody] CretaeBookStoreDto cretaeBookStoreDto)
        {
            var bookStoreId = _bookStoreService.CreateBookStore(cretaeBookStoreDto);

            return Created($"api/bookStore/{bookStoreId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBookStore([FromRoute] int bookStoreId)
        {
            var isDeleted = _bookStoreService.DeleteBookStore(bookStoreId);

            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateBookStore([FromBody]UpdateBookStoreDto dto, [FromRoute] int id)
        {
            var isBookStoreUpdated = _bookStoreService.UpdateBookStore(dto, id);
            if (!isBookStoreUpdated)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
