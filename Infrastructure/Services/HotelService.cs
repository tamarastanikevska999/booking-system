using Core.DomainModels;
using Core.DTO;
using Core.Enums;
using Core.Interfaces;
using Infrastructure.SharedKernel.Constants;
using Infrastructure.SharedKernel.Validators;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class HotelService : IHotelService
{
    private readonly MemoryStorageService _memoryStorageService;
    private readonly BookingValidator _bookingValidator;

    public HotelService(MemoryStorageService memoryStorageService, BookingValidator bookingValidator)
        {
            _bookingValidator = bookingValidator;
            _memoryStorageService = memoryStorageService;
    }

    public async Task<List<Hotel>> SearchHotels(SearchRequest request)
    {
        using var httpClient = new HttpClient();
            var optionsJson = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            var response = await httpClient.GetAsync($"{ConfigurationConstants.hotelsDataEndpoint}?destinationCode={request.Destination}");
            var content = await response.Content.ReadAsStringAsync();
            var hotels = JsonSerializer.Deserialize<List<Hotel>>(content, optionsJson);
            return hotels;
    }

    public async Task<Booking> BookHotel(BookRequest request)
    {
        var bookingCode = _bookingValidator.GenerateRandomCode(6);
        var sleepTime = new Random().Next(30, 61);
        var bookingTime = DateTime.Now;

        var bookingResponse = new Booking
        {
            BookingCode = bookingCode,
            BookingTime = bookingTime,
            SleepTime = sleepTime,
            Status = BookingStatusEnum.Pending
        };
            return bookingResponse;
        }

        public async Task<CheckStatusResponse> CheckHotelStatus(CheckStatusRequest request)
        {
            var status = _memoryStorageService.GetBookingStatus(request.BookingCode);
            return status;
        }

    }
}
