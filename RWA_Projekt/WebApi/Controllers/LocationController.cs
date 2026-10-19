using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;
namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private static List<Location> _Locations = new List<Location>();
        private static int _nextId = 1;

        [HttpPost]
        public ActionResult<Location> CreateLocation(Location locations)
        {
           locations.Id = _nextId;
            _nextId++;
            _Locations.Add(locations);
            LogsRepository.AddLogs(0, "Lokacija kreirana");

            return Ok(locations);


        }


        [HttpGet]
        public ActionResult<List<Location>> GetAllLocations()
        {
            try
            {
                return Ok(_Locations);
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
                var location = _Locations.FirstOrDefault(b => b.Id == id);

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
        public ActionResult<Location> UpdateLocationkById(int id, Location location)
        {
            try
            {
                var existing = _Locations.FirstOrDefault(b => b.Id == id);

                if (existing == null)
                {


                    return NotFound($"Nema lokacije s id-em {id}");
                }


                existing.Name = location.Name;
                existing.Address = location.Address;

                

                LogsRepository.AddLogs(0, "Location updated");

                return Ok(existing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }






        [HttpDelete("{id}")]
        public ActionResult RemoveLocation(int id)
        {
            try
            {

                var location = _Locations.FirstOrDefault(b => b.Id == id);
                if (location == null)
                {


                    return NotFound($"Ne postoji lokacija s id-em {id}");

                }
                _Locations.Remove(location);
                LogsRepository.AddLogs(0, "Lokacija obrisana");


                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }
}
