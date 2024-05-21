using Core.DTO;
using Core.Enums;
using Core.Interfaces;
using Infrastructure.SharedKernel.Mappers;

namespace Infrastructure.Services.Managers
{
    public class HotelManager : BaseHotelManager
    {
        public HotelManager(IHotelService hotelService, MemoryStorageService memoryStorageService, BookingMappers bookingMappers)
            : base(hotelService, memoryStorageService, bookingMappers)
        {
        }

        public override async Task<BookResponse> Book(BookRequest request)
        {
            var bookingResponse = await base.Book(request);

            var booking = _memoryStorageService.GetBooking(bookingResponse.BookingCode);
            _ = Task.Run(async () =>
            {
                await Task.Delay(booking.SleepTime * 1000);
                var status = new CheckStatusResponse
                {
                    Status = BookingStatusEnum.Success
                };
                _memoryStorageService.SaveBookingStatus(booking.BookingCode, status);
            });

            return bookingResponse;
        }
    }

}
