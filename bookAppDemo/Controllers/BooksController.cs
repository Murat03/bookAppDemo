using bookAppDemo.Data;
using bookAppDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        public IActionResult UpdateOneBook(
            [FromRoute(Name = "id")]int id, [FromBody] Book book)
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
        
        [HttpDelete]
        public IActionResult DeleteAllBooks()
        {
            ApplicationContext.Books.Clear();
            return NoContent(); //204
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")]int id)
        {
            var entity = ApplicationContext.Books.Find(a => a.Id == id);
            if(entity is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    message = $"Book with id:{id} could not found."
                }); //404
            }
            ApplicationContext.Books.Remove(entity);
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")]int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            //check entity
            var entity = ApplicationContext.Books.Find(b => b.Id.Equals(id));

            if(entity == null)
            {
                return NotFound(); //404
            }
            bookPatch.ApplyTo(entity);
            return NoContent(); //204
        }
    }
}
