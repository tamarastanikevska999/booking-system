using Core.DTO;
using Core.Interfaces;
using Infrastructure.SharedKernel.Mappers;


namespace Infrastructure.Services.Managers
{
    public abstract class BaseHotelManager : IManager
    {
        protected readonly IHotelService _hotelService;
        protected readonly MemoryStorageService _memoryStorageService;
        protected readonly BookingMappers _bookingMappers;

        public BaseHotelManager(IHotelService hotelService, MemoryStorageService memoryStorageService, BookingMappers bookingMappers)
        {
            _hotelService = hotelService;
            _memoryStorageService = memoryStorageService;
            _bookingMappers = bookingMappers;
        }

        public virtual async Task<SearchResponse> Search(SearchRequest request)
        {
            var response = await _hotelService.SearchHotels(request);
            var options = response.Select(_bookingMappers.MapHotelToOption).ToList();
            var searchResponse = new SearchResponse { Options = options };

            _memoryStorageService.SaveSearchResult(request.Destination, searchResponse);
            return searchResponse;
        }

        public virtual async Task<BookResponse> Book(BookRequest request)
        {
            var response = await _hotelService.BookHotel(request);
            var bookResponse = new BookResponse { BookingCode = response.BookingCode, BookingTime = response.BookingTime };
            _memoryStorageService.SaveBookingStatus(response.BookingCode, new CheckStatusResponse
            {
                Status = response.Status
            });
            _memoryStorageService.SaveBooking(response.BookingCode, response);
            return bookResponse;
        }

        public virtual async Task<CheckStatusResponse> CheckStatus(CheckStatusRequest request)
        {
            var status = _memoryStorageService.GetBookingStatus(request.BookingCode);
            return status;
        }
    }
}
