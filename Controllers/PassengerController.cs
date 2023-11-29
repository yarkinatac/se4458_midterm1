using Microsoft.AspNetCore.Mvc;
using Repository;

namespace Controllers
{
     [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    
    public class PassengerController : ControllerBase
    {
       private  IConfiguration _configuration;
        public PassengerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Login modal)
        {
            var passengerRepository = new PassengerRepository(_configuration);
            var passenger = passengerRepository.GetPassengerLogin(modal);
            return Ok(passenger);
        }

        [HttpPost]
        public IActionResult SignUp([FromBody]SignUp modal){
            var passengerRepository = new PassengerRepository(_configuration);
            var passenger=passengerRepository.CreatePassenger(modal);
            return Ok(passenger);
        }
    }
}