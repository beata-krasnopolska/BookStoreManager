using BookStoreManager.Models;
using BookStoreManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookStoreManager.Controllers
{
    [Route("api/bookStore")]
    [ApiController]
    [Authorize]
    public class BookStoreController : ControllerBase
    {
        private readonly IBookStoreService _bookStoreService;

        public BookStoreController(IBookStoreService bookStoreService)
        {
            _bookStoreService = bookStoreService;
        }

        [HttpGet]
        [Authorize(Policy = "HasBirthDate")]
        public ActionResult<IEnumerable<BookStoreDto>> GetAllBookStores([FromQuery] string searchParam)
        {
            var bookStores = _bookStoreService.GetAllBookStores(searchParam);

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
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult<int> CreateBookStore([FromBody] CretaeBookStoreDto cretaeBookStoreDto)
        {
            var bookStoreId = _bookStoreService.CreateBookStore(cretaeBookStoreDto);

            return Created($"api/bookStore/{bookStoreId}", null);
        }

        [HttpDelete("{bookStoreId}")]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult DeleteBookStore([FromRoute] int bookStoreId)
        {
            _bookStoreService.DeleteBookStore(bookStoreId);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult UpdateBookStore([FromBody]UpdateBookStoreDto dto, [FromRoute] int id)
        {
            _bookStoreService.UpdateBookStore(dto, id);

            return Ok();
        }
    }
}
