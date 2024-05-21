

using Core.DTO;
using Core.Entities;

namespace Infrastructure.SharedKernel.Mappers
{
    public class BookingMappers
    {
        public Option MapHotelToOption(Hotel hotel)
        {
            return new Option
            {
                OptionCode = Guid.NewGuid().ToString(),
                HotelCode = hotel.HotelCode.ToString(),
            };
        }

        public Option MapFlightToOption(Flight flight)
        {
            return new Option
            {
                OptionCode = Guid.NewGuid().ToString(),
                FlightCode = flight.FlightCode.ToString(),
                ArrivalAirport = flight.ArrivalAirport,
                Price = 50
            };
        }
    }
}
