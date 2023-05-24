using bookAppDemo.Data;
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
    }
}
