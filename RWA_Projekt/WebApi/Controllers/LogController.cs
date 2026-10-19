using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : Controller
    {

        [HttpGet]
        public ActionResult<IEnumerable<Log>> Get(int last = 20)
        {
            try
            {
                var result = LogsRepository.LogsItems.TakeLast(last);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }




        [HttpGet("count")]
        public ActionResult<int> Count()
        {

            try
            {

                return Ok(LogsRepository.LogsItems.Count);
            }


            catch (Exception ex)
            {

               return  StatusCode(500, ex.Message);
            }
        }


    }
}
