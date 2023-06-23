using AutoMapper;
using BookStoreManager.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreManager.Controllers
{
    [Route("api/bookStore")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext;
        public BookStoreController(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookStore>> GetAllBookStores()
        {
            var bookStores = _dbContext.BookStores.ToList();

            return Ok(bookStores);
        }

        [HttpGet("{id}")]
        public ActionResult<BookStore> GetBookStoresBNyId([FromRoute] int id)
        {
            var bookStore = _dbContext.BookStores.FirstOrDefault(b => b.Id == id);

            if (bookStore is null)
            {
                return NotFound();
            }

            return Ok(bookStore);
        }
    }
}
