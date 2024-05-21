using Core.DTO;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.SharedKernel.Constants;
using Infrastructure.SharedKernel.Validators;
using System.Text.Json;


namespace Infrastructure.Services
{
    public class FlightService : IFlightService
    {
        private readonly MemoryStorageService _memoryStorageService;
        private readonly BookingValidator _bookingValidator;

        public FlightService(MemoryStorageService memoryStorageService, BookingValidator bookingValidator)
        {
            _bookingValidator = bookingValidator;
            _memoryStorageService = memoryStorageService;
        }
        public async Task<List<Flight>> SearchFlights(SearchRequest request)
        {

            using var httpClient = new HttpClient();
            var optionsJson = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var response = await httpClient.GetStringAsync($"{ConfigurationConstants.flightsDataEndpoint}?departureAirport={request.DepartureAirport}&arrivalAirport={request.Destination}");
            var flights = JsonSerializer.Deserialize<List<Flight>>(response, optionsJson);

            return flights;
        }

        public async Task<BookResponse> BookFlight(BookRequest request)
        {
            var bookingCode = _bookingValidator.GenerateRandomCode(6);
            var sleepTime = new Random().Next(30, 61);
            await Task.Delay(sleepTime * 1000);
            return new BookResponse { BookingCode = bookingCode, BookingTime = DateTime.Now };
        }

        public async Task<CheckStatusResponse> CheckFlightStatus(CheckStatusRequest request)
        {
            var status = _memoryStorageService.GetBookingStatus(request.BookingCode);
            return status;
        }

    }

}
