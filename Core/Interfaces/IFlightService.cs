using Core.DTO;

namespace Core.Interfaces
{
    public interface IFlightService
    {
        Task<List<Flight>> SearchFlights(SearchRequest request);
        Task<BookResponse> BookFlight(BookRequest request);
        Task<CheckStatusResponse> CheckFlightStatus(CheckStatusRequest request);
    }
}
