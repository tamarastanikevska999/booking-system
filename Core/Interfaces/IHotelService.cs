using Core.DomainModels;
using Core.DTO;

namespace Core.Interfaces
{
    public interface IHotelService
    {
        Task<List<Hotel>> SearchHotels(SearchRequest request);
        Task<Booking> BookHotel(BookRequest request);
        Task<CheckStatusResponse> CheckHotelStatus(CheckStatusRequest request);
    }
}
