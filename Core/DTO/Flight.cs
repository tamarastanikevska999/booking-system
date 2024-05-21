namespace Core.DTO
{
    public class Flight
    {
        public int FlightCode { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public double Price { get; set; } = 50;

    }
}
