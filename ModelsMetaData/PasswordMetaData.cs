using System.ComponentModel.DataAnnotations;

namespace ElSayedHotel.ModelsMetaData
{
    public class PasswordMetaData
    {
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[-@$!%*?&_])[A-Za-z\d-@$!%*?&_]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string password { get; set; }

    }
}
