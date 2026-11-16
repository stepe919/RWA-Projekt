using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Repositories;
namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {

        private readonly RwaLibraryLearnContext _context;


        public LocationController(RwaLibraryLearnContext context)
        {

            _context = context;
        }


        [HttpPost]
        [Authorize]
        public ActionResult<Location> CreateLocation(Location location)
        {
            try
            {
                var _location = new Location
                {
                    Name = location.Name,
                    Address = location.Address
                 
                };

                _context.Add(_location);
                _context.SaveChanges();

                LogsRepository.AddLogs(_context, 0, "Location created");

                location.Id = _location.Id;

                return Ok(_location);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet]
 
        public ActionResult<List<Location>> GetAllLocations()
        {
            try
            {
                return Ok(_context.Locations.ToList());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
  
        public ActionResult<Location> GetByIdLocation(int id)
        {
            try
            {
                var location = _context.Locations.FirstOrDefault(b => b.Id == id);

                if (location == null)
                {


                    return NotFound($"Nema lokacije s id-em {id}");
                }






                return Ok(location);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Location> UpdateLocationById(int id, Location location)
        {
            try
            {
                var existing = _context.Locations.FirstOrDefault(b => b.Id == id);

                if (existing == null)
                {


                    return NotFound($"Nema lokacije s id-em {id}");
                }


                existing.Name = location.Name;
                existing.Address = location.Address;


                _context.SaveChanges();

                LogsRepository.AddLogs(_context, 0, "Location updated");

                return Ok(existing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }






        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult RemoveLocation(int id)
        {
            try
            {

                var location = _context.Locations.FirstOrDefault(b => b.Id == id);
                if (location == null)
                {


                    return NotFound($"Ne postoji lokacija s id-em {id}");

                }


                if (_context.BookLocations.Any(bl => bl.LocationId == id))
                {
                    return BadRequest($"Location with id {id} has books and cannot be deleted");
                }

                _context.Remove(location);
                _context.SaveChanges();

                LogsRepository.AddLogs(_context, 0, "Lokacija obrisana");


                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }
}
