using Half_Checkmate.Models.Half_Checkmate.Models;
using System.ComponentModel.DataAnnotations;

namespace Half_Checkmate.Models
{
    public class TblUsers
    {
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Minimum 2 chars")]
        public string? Name { get; set; }

        [Key]
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [PhoneNumberValidationAttribute]
        public string? Phone { get; set; }


        [Required(ErrorMessage = "Please select a country.")]
        public string? Country { get; set; }

        [Display(Name = "Number Of Games")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of games cannot be negative.")]
        public int NumberOfGames { get; set; }

    }
}
