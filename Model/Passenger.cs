using System;
using System.Collections.Generic;

namespace midterm.Model;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Username { get; set; }

    public string? PassengerPassword { get; set; }

    public string? Token { get; set; }

    public virtual ICollection<PassengerFlight> PassengerFlights { get; } = new List<PassengerFlight>();
}
