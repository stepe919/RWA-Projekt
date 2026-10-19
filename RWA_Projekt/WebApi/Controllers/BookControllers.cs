using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> _books = new List<Book>();
        private static int _nextId = 1;


        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            book.Id = _nextId;
            _nextId++;
            _books.Add(book);
            LogsRepository.AddLogs(0, "Book created");

            return Ok(book);


        }

        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            try
            {
                return Ok(_books);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpGet("{id}")]
        public ActionResult<Book> GetByIdBook(int id)
        {
            try
            {
                var book = _books.FirstOrDefault(b => b.Id == id);

                if (book == null)
                {


                    return NotFound($"Nema knjige s id-em {id}");
                }






                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }








        [HttpPut("{id}")]
        public ActionResult<Book> UpdateBookById(int id, Book book)
        {
            try
            {
                var existing = _books.FirstOrDefault(b => b.Id == id);

                if (existing == null)
                {


                    return NotFound($"Nema knjige s id-em {id}");
                }


                existing.Author = book.Author;
                existing.Description = book.Description;
                existing.PublishedYear = book.PublishedYear;
                existing.Name = book.Name;
                existing.GenreId = book.GenreId;

                LogsRepository.AddLogs(0, "Book updated");

                return Ok(existing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }






        [HttpDelete("{id}")]
        public ActionResult RemoveBooks(int id)
        {
            try
            {

                var book = _books.FirstOrDefault(b => b.Id == id);
                if (book == null)
                {


                    return NotFound($"Ne postoji knjiga s id-em {id}");

                }
                _books.Remove(book);
                LogsRepository.AddLogs(0, "Book deleted");


                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("[action]")]

        public ActionResult<List<Book>> SearchBooks(string name, int page = 1, int count = 10)
        {

            var results = _books;

            if (!string.IsNullOrEmpty(name))
            {

                results = results.Where(b => b.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            }

            results = results.Skip((page - 1) * count).Take(count).ToList();

            return Ok(results);

        }
    }




}
