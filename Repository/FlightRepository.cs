
using Microsoft.EntityFrameworkCore;
using midterm.Model;

namespace Repository
{
    public class FlightsRepository
    {
        public  List<Flight> GetFlights(TicketQuery modal)
        {
            using var context = new Se4458midtermContext();
            var filter = new PaginationManager(modal.PageNumber, modal.PageSize);

            var pagedData = context.Flights
                .Where(flight => flight.FlightDate == modal.Date && flight.Departure == modal.Departure && flight.Destination == modal.Destination && flight.AvailableSeat >= modal.SoldTicket)
                .Skip((filter.minNumber - 1) * filter.maxNumber)
                .Take(filter.maxNumber)
                .ToList();

            return pagedData;
        }

        public Flight? UpdateFlightPassengers(int id)
        {
            using var context = new Se4458midtermContext();
            try
            {
                var flight = context.Flights.Find(id);
                if (flight != null)
                {
                    if (flight.AvailableSeat >= 1)
                    {
                        flight.AvailableSeat--;
                        context.Flights.Update(flight);
                        context.SaveChanges();
                    }
                }
                return flight;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Flight CreateFilght(Flight f)
        {
            var flight = new Flight { FlightDate = f.FlightDate, FlightNumber = f.FlightNumber, Departure = f.Departure, Destination = f.Destination, AvailableSeat = f.AvailableSeat, TotalSeat = f.TotalSeat, Price = f.Price };
            using var context = new Se4458midtermContext();
            context.Flights.Add(flight);
            context.SaveChanges();
            return f;
        }

        public string BuyTicket(BuyTicket ticket, Passenger client)
{
    var status = "";
    using var context = new Se4458midtermContext();

    // Use FirstOrDefault instead of First
    var flight = context.Flights.FirstOrDefault(x => x.FlightNumber == ticket.FlightNo);
    if (flight != null)
    {
        // Use FirstOrDefault instead of First
        var flightClient = context.PassengerFlights.FirstOrDefault(x => x.FlightId == flight.FlightId && x.PassengerId == client.PassengerId);

        if (flightClient != null)
        {
            status = "Client has already on the flight";
        }
        else
        {
            var entity = context.PassengerFlights.Add(new PassengerFlight { PassengerId = client.PassengerId, FlightId = flight.FlightId });
            context.SaveChanges();

            if (entity != null)
            {
                var updatedFlight = UpdateFlightPassengers(flight.FlightId);
                if (updatedFlight != null && updatedFlight.AvailableSeat < flight.AvailableSeat)
                {
                    status = "Passenger assigned to flight.";
                }
                else
                {
                    status = "There are no available seats :( ";
                }
            }
            else
            {
                status = "Passenger can't be assigned to the flight";
            }
        }
    }
    else
    {
        status = "Flight cannot be found!";
    }
    return status;
}

    }
}