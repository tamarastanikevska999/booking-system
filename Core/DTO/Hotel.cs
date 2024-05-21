using System.Runtime.Serialization;

namespace Core.DTO
{
    public class Hotel
    {
        public int Id { get; set; }
        [DataMember(Name = "hotelCode", EmitDefaultValue = false)]
        public int HotelCode { get; set; }
        public string HotelName { get; set; }
        public string DestinationCode { get; set; }
        public string City { get; set; }
        public double Price { get; set; } = 100;
    }
}
