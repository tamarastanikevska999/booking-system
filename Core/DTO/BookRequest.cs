using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class BookRequest
    {
        [Required]
        public string OptionCode { get; set; }
        [Required]
        public SearchRequest SearchRequest { get; set; }
    }
}
