using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using WebApi.Repositories;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {


        private readonly RwaLibraryLearnContext _context;


        public BookController(RwaLibraryLearnContext context)
        {

            _context = context;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<BookDto> CreateBook(BookDto bookDto)
        {
            try
            {

                if (!_context.Genres.Any(g => g.Id == bookDto.GenreId))
                {
                    return BadRequest($"Žanr s Id-em {bookDto.GenreId} ne postoji");
                }
                var book = new Book
                {
                    Name = bookDto.Name,
                    Author = bookDto.Author,
                    Isbn = bookDto.Isbn,
                    Description = bookDto.Description,
                    PublishedYear = bookDto.PublishedYear,
                    GenreId = bookDto.GenreId
                };

                _context.Add(book);


                _context.SaveChanges();

                LogsRepository.AddLogs(_context,0, "Book created");

                bookDto.Id = book.Id;

                return Ok(bookDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet]
      
        public ActionResult<List<Book>> GetAllBooks()
        {
            try
            {
                return Ok(_context.Books.ToList());
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
                var book = _context.Books.FirstOrDefault(b => b.Id == id);

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
        [Authorize]
        public ActionResult<Book> UpdateBookById(int id, BookDto bookDto)
        {
            try
            {
                var existing = _context.Books.FirstOrDefault(b => b.Id == id);

                if (existing == null)
                {


                    return NotFound($"Nema knjige s id-em {id}");
                }

                existing.Name = bookDto.Name;
                existing.Author = bookDto.Author;
                existing.Description = bookDto.Description;
                existing.PublishedYear = bookDto.PublishedYear;
                existing.Isbn = bookDto.Isbn;
                existing.GenreId = bookDto.GenreId;
                
                _context.SaveChanges();

                LogsRepository.AddLogs(_context,0, "Book updated");
                return Ok(existing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }






        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult RemoveBooks(int id)
        {
            try
            {

                var book = _context.Books.FirstOrDefault(b => b.Id == id);
                if (book == null)
                {


                    return NotFound($"Ne postoji knjiga s id-em {id}");

                }
                _context.Remove(book);

                _context.SaveChanges();

                LogsRepository.AddLogs(_context, 0, "Book deleted");


                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("[action]")]
  

        public ActionResult<List<Book>> SearchBooks(string? name, int page = 1, int count = 10)
        {

            IQueryable<Book> results = _context.Books;


         

            try
            {




                if (!string.IsNullOrEmpty(name))
                {

                    results = results.Where(b => b.Name.Contains(name));
                }

                results = results.OrderBy(b => b.Name);

                results = results.Skip((page - 1) * count).Take(count);

                return Ok(results.ToList());
            }


            catch (Exception ex)
            {

               



                    return StatusCode(500,ex.Message);
            }
        }
    }




}
