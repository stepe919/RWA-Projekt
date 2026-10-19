using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;
namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GenreController :ControllerBase
    {
        private static List<Genre> _genres = new List<Genre>();
        private static int _nextId = 1;

        [HttpPost]
        public ActionResult<Genre> CreateBook(Genre genres)
        {
            genres.Id = _nextId;
            _nextId++;
            _genres.Add(genres);
            LogsRepository.AddLogs(0, "Book created");

            return Ok(genres);


        }


        [HttpGet]
        public ActionResult<List<Book>> GetAllGenres()
        {
            try
            {
                return Ok(_genres);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Genre> GetByIdGenre(int id)
        {
            try
            {
                var book = _genres.FirstOrDefault(b => b.Id == id);

                if (book == null)
                {


                    return NotFound($"Nema knjige s id-em {id}");
                }






                return Ok(_genres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




    }
}
