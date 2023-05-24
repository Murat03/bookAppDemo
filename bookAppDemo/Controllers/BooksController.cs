using bookAppDemo.Data;
using bookAppDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookAppDemo.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var result = ApplicationContext.Books;

            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")]int id)
        {
            var result = ApplicationContext.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        [HttpPost]
        public IActionResult CreateOneBook([FromBody]Book book)
        {
            if(book == null)
            {
                return BadRequest();
            }

            ApplicationContext.Books.Add(book);
            return StatusCode(201, book);
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")]int id,
            [FromBody] Book book)
        {
            //check book?
            var entity = ApplicationContext.Books.Find(b => b.Id == id);

            if(entity is null)
                return NotFound(); // 404
            
            //check id
            if(book.Id != id)
                return BadRequest(); // 400

            ApplicationContext.Books.Remove(entity);

            ApplicationContext.Books.Add(book);

            return NoContent();

        }
    }
}
