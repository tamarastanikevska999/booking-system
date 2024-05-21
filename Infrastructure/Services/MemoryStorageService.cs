using Core.DomainModels;
using Core.DTO;


namespace Infrastructure.Services
{
    public class MemoryStorageService
    {
        private readonly Dictionary<string, SearchResponse> _searchResults = new();
        private readonly Dictionary<string, Booking> _bookings = new();
        private readonly Dictionary<string, CheckStatusResponse> _bookingStatuses = new();

        public void SaveSearchResult(string key, SearchResponse response)
        {
            _searchResults[key] = response;
        }

        public SearchResponse GetSearchResult(string key)
        {
            return _searchResults.TryGetValue(key, out var response) ? response : null;
        }

        public void SaveBooking(string key, Booking response)
        {
            _bookings[key] = response;
        }

        public BookResponse GetBookResponse(string key)
        {
            var book = _bookings.TryGetValue(key, out var response) ? response : null;
            if (book == null)
            {
                return new BookResponse
                {
                    BookingTime = book.BookingTime,
                    BookingCode = book.BookingCode,
                };
            }
            return null;
        }

        public Booking GetBooking(string key)
        {
            return _bookings.TryGetValue(key, out var response) ? response : null;
        }

        public void SaveBookingStatus(string key, CheckStatusResponse response)
        {
            _bookingStatuses[key] = response;
        }

        public CheckStatusResponse GetBookingStatus(string key)
        {
            return _bookingStatuses.TryGetValue(key, out var response) ? response : null;
        }

        public bool ContainsSearchResult(string key)
        {
            return _searchResults.ContainsKey(key);
        }

        public bool ContainsBooking(string key)
        {
            return _bookings.ContainsKey(key);
        }

        public bool ContainsBookingStatus(string key)
        {
            return _bookingStatuses.ContainsKey(key);
        }
    }

}
