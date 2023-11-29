using midterm.Model;

namespace Repository
{
    public class PassengerFlightRepository 
    {
       public bool IsAvailableToFlight(int userId, int flightId)
        {
            using var context = new Se4458midtermContext();
            try
            {
                var userFlight = context.PassengerFlights.First(u => u.PassengerId == userId && u.FlightId == flightId);
                return userFlight != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}