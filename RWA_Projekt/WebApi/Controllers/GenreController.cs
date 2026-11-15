using Microsoft.AspNetCore.Authorization;
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
    
        private readonly RwaLibraryLearnContext _context;


        public GenreController(RwaLibraryLearnContext context)
        {
           
            _context = context;
        }


        [HttpPost]
        [Authorize]
        public ActionResult<Genre> CreateGenre(Genre genre )
        {
          
            _context.Add(genre);
           


            try
            {
                _context.SaveChanges();
                LogsRepository.AddLogs(_context, 0, "Genre created");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }


            return Ok(genre);


          

        }


        [HttpGet]
    
        public ActionResult<List<Genre>> GetAllGenres()
        {
            try
            {
                return Ok(_context.Genres.ToList());
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
                var genre = _context.Genres.FirstOrDefault(b => b.Id == id);

                if (genre == null)
                {


                    return NotFound($"No genre with id {id}");
                }






                return Ok(genre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult RemoveGenres(int id)
        {
            try
            {

                if (_context.Books.Any(b => b.GenreId == id))
                {
                    return BadRequest($"Žanr ima knjige s tim ide-em pa se ne smije obrisati");
                }

                var genre = _context.Genres.FirstOrDefault(b => b.Id == id);
                if ( genre == null)
                {


                    return NotFound($"Ne postoji genre s id-em {id}");

                }
                _context.Remove(genre);
                _context.SaveChanges();
                LogsRepository.AddLogs(_context, 0, "Genre deleted");



                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}")]
        [Authorize]
        public ActionResult <Genre> UpdateBookById(int id, Genre genre)
        {
            try
            {
                var existing = _context.Genres.FirstOrDefault(b => b.Id == id);

                if (existing == null)
                {


                    return NotFound($"Nema genre-a s id-em {id}");
                }


                existing.Name = genre.Name;
                existing.Description = genre.Description;

                _context.SaveChanges();
                LogsRepository.AddLogs(_context, 0, "Genre updated");

                return Ok(existing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




    }
}
