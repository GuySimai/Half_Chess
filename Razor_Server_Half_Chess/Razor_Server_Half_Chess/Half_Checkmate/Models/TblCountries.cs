using System.ComponentModel.DataAnnotations;

namespace Half_Checkmate.Models
{
    public class TblCountries
    {
        [Key]
        public string? Country { get; set; }
    }
}
