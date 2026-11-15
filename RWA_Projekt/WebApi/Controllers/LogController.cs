using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;



namespace WebApi.Controllers
{
    [Route("api/logs")]
    [ApiController]
    public class LogController : ControllerBase
    {

        private readonly RwaLibraryLearnContext _context;



        public LogController(RwaLibraryLearnContext context)
        {

            _context = context;
        }

        [HttpGet("get/{last}")]
        public ActionResult<List<Log>> Get(int last)
        {
            try
            {
                var result = _context.Logs
                             .OrderByDescending(l=>l.Timestamp)
                             .Take(last)
                             .ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpGet("count")]
        public ActionResult<int> Count()
        {

            try
            {

                return Ok(_context.Logs.Count());
            }


            catch (Exception ex)
            {

               return  StatusCode(500, ex.Message);
            }
        }


    }
}
