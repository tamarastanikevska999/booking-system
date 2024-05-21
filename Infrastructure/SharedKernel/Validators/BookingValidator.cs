using Core.DTO;
using Core.Entities;
using Core.Enums;


namespace Infrastructure.SharedKernel.Validators
{
    public class BookingValidator
    {
        public bool IsLastMinuteHotelSearch(DateTime fromDate)
        {
            return (fromDate - DateTime.Now).TotalDays <= 45;
        }

        public BookingStatusEnum CombineStatuses(BookingStatusEnum hotelStatus, BookingStatusEnum flightStatus)
        {
            if (hotelStatus == BookingStatusEnum.Failed || flightStatus == BookingStatusEnum.Failed)
            {
                return BookingStatusEnum.Failed;
            }
            else if (hotelStatus == BookingStatusEnum.Pending || flightStatus == BookingStatusEnum.Pending)
            {
                return BookingStatusEnum.Pending;
            }
            else
            {
                return BookingStatusEnum.Success;
            }
        }

        public string GenerateSearchResultKey(SearchRequest request)
        {
            return $"{request.Destination}-{request.DepartureAirport}-{request.FromDate}-{request.ToDate}";
        }

        public string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public List<Option> CombineHotelAndFlightOptions(List<Hotel> hotels, List<Flight> flights)
        {
            var options = new List<Option>();

            foreach (var hotel in hotels)
            {
                var matchingFlights = flights.Where(f => f.ArrivalAirport == hotel.DestinationCode).ToList();

                foreach (var flight in matchingFlights)
                {
                    options.Add(new Option
                    {
                        OptionCode = $"{hotel.HotelCode}-{flight.FlightCode}",
                        HotelCode = hotel.HotelCode.ToString(),
                        FlightCode = flight.FlightCode.ToString(),
                        ArrivalAirport = flight.ArrivalAirport
                    });
                }
            }

            return options;
        }
    }
}
