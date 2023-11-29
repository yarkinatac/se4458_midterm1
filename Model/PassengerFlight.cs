using System;
using System.Collections.Generic;

namespace midterm.Model;

public partial class PassengerFlight
{
    public int Id { get; set; }

    public int? PassengerId { get; set; }

    public int? FlightId { get; set; }

    public virtual Flight? Flight { get; set; }

    public virtual Passenger? Passenger { get; set; }
}
