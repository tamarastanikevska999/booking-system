using Core.DomainModels;
using Core.DTO;
using Core.Enums;
using Core.Interfaces;
using Infrastructure.SharedKernel.Validators;


namespace Infrastructure.Services.Managers
{
    public class HotelFlightManager : IManager
    {
        private readonly IHotelService _hotelService;
        private readonly IFlightService _flightService;
        private readonly MemoryStorageService _memoryStorageService;
        private readonly BookingValidator _bookingValidator;


        public HotelFlightManager(IHotelService hotelService, IFlightService flightService, MemoryStorageService memoryStorageService, BookingValidator bookingValidator)
        {
            _bookingValidator = bookingValidator;
            _hotelService = hotelService;
            _flightService = flightService;
            _memoryStorageService = memoryStorageService;
        }

        public async Task<SearchResponse> Search(SearchRequest request)
        {
            var searchResultKey = _bookingValidator.GenerateSearchResultKey(request);
            if (!_memoryStorageService.ContainsSearchResult(searchResultKey))
            {
                var hotelSearchResult = await _hotelService.SearchHotels(request);
                var flightSearchResult = await _flightService.SearchFlights(request);
                var combinedOptions = _bookingValidator.CombineHotelAndFlightOptions(hotelSearchResult, flightSearchResult);
                var searchResponse = new SearchResponse { Options = combinedOptions };
                _memoryStorageService.SaveSearchResult(searchResultKey, searchResponse);
            }
            return _memoryStorageService.GetSearchResult(searchResultKey);
        }



        public async Task<BookResponse> Book(BookRequest request)
        {
            var bookingKey = request.OptionCode;
            if (!_memoryStorageService.ContainsBooking(bookingKey))
            {
                var hotelBookingResult = await _hotelService.BookHotel(request);
                var flightBookingResult = await _flightService.BookFlight(null);
                var combinedBooking = new Booking { BookingCode = hotelBookingResult.BookingCode+ flightBookingResult.BookingCode, BookingTime = DateTime.Now };
                _memoryStorageService.SaveBooking(bookingKey, combinedBooking);
                var booking = _memoryStorageService.GetBooking(combinedBooking.BookingCode);
                _ = Task.Run(async () =>
                {
                    await Task.Delay(booking.SleepTime * 1000);
                    var status = new CheckStatusResponse
                    {
                        Status = BookingStatusEnum.Success
                    };
                    _memoryStorageService.SaveBookingStatus(booking.BookingCode, status);
                });
            }
            return _memoryStorageService.GetBookResponse(bookingKey);
        }

        public async Task<CheckStatusResponse> CheckStatus(CheckStatusRequest request)
        {
            if (!_memoryStorageService.ContainsBookingStatus(request.BookingCode))
            {
                var hotelStatusResult = await _hotelService.CheckHotelStatus(request);
                var flightStatusResult = await _flightService.CheckFlightStatus(request);
                var combinedStatus = new CheckStatusResponse { Status = _bookingValidator.CombineStatuses(hotelStatusResult.Status, flightStatusResult.Status) };
                _memoryStorageService.SaveBookingStatus(request.BookingCode, combinedStatus);
            }
            return _memoryStorageService.GetBookingStatus(request.BookingCode);
        }
    }
}
