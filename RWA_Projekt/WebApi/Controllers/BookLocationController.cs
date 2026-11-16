using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Repositories;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class BookLocationController:ControllerBase
    {
       
        private readonly RwaLibraryLearnContext _context;


        public BookLocationController(RwaLibraryLearnContext context)
        {

            _context = context;
        }



        [HttpPost]
        [Authorize]
        public ActionResult<BookLocationDto> AddBookToLocation(BookLocationDto bookLocationDto)
        {
            try
            {
                if (!_context.Locations.Any(l=>l.Id == bookLocationDto.LocationId)) { 
                
                
                 return (StatusCode( 404, "Location not found"));
                
                }



                if (!_context.Books.Any(b => b.Id == bookLocationDto.BookId))
                {


                    return (StatusCode(404, "Book not found"));

                }



                if (_context.BookLocations.Any(bl => bl.BookId == bookLocationDto.BookId
                                   && bl.LocationId == bookLocationDto.LocationId))
                {
                    return BadRequest("This book is already in this location");
                }




                var bookLocation = new BookLocation{


                    BookId = bookLocationDto.BookId,
                    LocationId = bookLocationDto.LocationId,
                    Quantity = bookLocationDto.Quantity
                    




                };

                _context.Add(bookLocation);
                _context.SaveChanges();
                bookLocationDto.Id = bookLocation.Id;
                LogsRepository.AddLogs(_context, 0, "Book added to Location");

           


                return Ok(bookLocationDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }





        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult RemoveBookFromLocation(int id)
        {
            try
            {
                var bookLocation = _context.BookLocations.FirstOrDefault(bl => bl.Id == id);

                if (bookLocation == null)
                {
                    return NotFound($"Book location with id {id} not found");
                }

                _context.Remove(bookLocation);
                _context.SaveChanges();

                LogsRepository.AddLogs(_context, 0, "Book removed from location");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
