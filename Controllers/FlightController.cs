using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using midterm.Model;
using Repository;

namespace Controllers
{
   [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]

    public class FlightController : ControllerBase
    {

        private IConfiguration _configuration;
        public FlightController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetFlights([FromQuery] TicketQuery modal)
        {
            var fligthRepo = new FlightsRepository();
            var resp = fligthRepo.GetFlights(modal);
            return Ok(resp);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateFlight([FromBody] Flight modal)
        {
            var fligthRepo = new FlightsRepository();
            var resp = fligthRepo.CreateFilght(modal);
            return Ok(resp);
        }

        [HttpPost, Authorize]
        public IActionResult BuyTicket([FromBody] BuyTicket modal)
        {
            var fligthRepo = new FlightsRepository();
            var passengerRepository = new PassengerRepository(_configuration);

            var token = TokenManager.GetToken(Request);

            var passenger = passengerRepository.GetPassengerByToken(token);
            if (passenger != null)
            {
                var response = fligthRepo.BuyTicket(modal, passenger);
                return Ok(response);
            }
            else
            {
                return Ok("Passenger token is not valid");
            }
        }
    }
}