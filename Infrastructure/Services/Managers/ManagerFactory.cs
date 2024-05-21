using Core.DTO;
using Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.SharedKernel.Validators;

namespace Infrastructure.Services.Managers
{
    public class ManagerFactory : IManagerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly BookingValidator _bookingValidator;

        public ManagerFactory(IServiceProvider serviceProvider, BookingValidator bookingValidator)
        {
            _serviceProvider = serviceProvider;
            _bookingValidator = bookingValidator;
        }

        public IManager CreateManager(SearchRequest request)
        {
            if (request.DepartureAirport == null)
            {
                return _serviceProvider.GetRequiredService<HotelManager>();
            }
            else if (_bookingValidator.IsLastMinuteHotelSearch(request.FromDate))
            {
                return _serviceProvider.GetRequiredService<LastMinuteHotelManager>();
            }
            else
            {
                return _serviceProvider.GetRequiredService<HotelFlightManager>();
            }
        }
    }
}

