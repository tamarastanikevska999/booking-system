

using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class CheckStatusRequest
    {
        [Required]
        public string BookingCode { get; set; }
    }
}
