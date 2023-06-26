using AutoMapper;
using BookStoreManager.Entities;
using BookStoreManager.Models;
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
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookStoreController(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookStoreDto>> GetAllBookStores()
        {
            var bookStores = _dbContext.BookStores.Include(b => b.Books).Include(a =>a.Address).ToList();

            if(bookStores == null)
            {
                return BadRequest();
            }

            var bookStoresDto = _mapper.Map<List<BookStoreDto>>(bookStores);

            return Ok(bookStoresDto);
        }

        [HttpGet("{id}")]
        public ActionResult<BookStoreDto> GetBookStoresBNyId([FromRoute] int id)
        {
            var bookStore = _dbContext.BookStores.Include(b => b.Books).Include(a => a.Address).FirstOrDefault(b => b.Id == id);

            if(bookStore is null)
            {
                return NotFound();
            }

            var bookStoreDto = _mapper.Map<BookStoreDto>(bookStore);

            return Ok(bookStoreDto);
        }

        [HttpPost]
        public ActionResult<int> CreateBookStore([FromBody] CretaeBookStoreDto cretaeBookStoreDto)
        {
            var bookStore = _mapper.Map<BookStore>(cretaeBookStoreDto);

            _dbContext.BookStores.Add(bookStore);
            _dbContext.SaveChanges();

            return Created($"api/bookStore/{bookStore.Id}", null);
        }
    }
}
