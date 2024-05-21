using Core.Enums;
using System.Text.Json.Serialization;

namespace Core.DTO
{
    public class CheckStatusResponse
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookingStatusEnum Status { get; set; }
    }

}
