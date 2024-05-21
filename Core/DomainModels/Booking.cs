using Core.Enums;
using System.Text.Json.Serialization;

namespace Core.DomainModels
{
    public class Booking
    {
        public string BookingCode { get; set; }
        public DateTime BookingTime { get; set; }
        public int SleepTime { get; set;}
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookingStatusEnum Status { get; set; }
    }
}
