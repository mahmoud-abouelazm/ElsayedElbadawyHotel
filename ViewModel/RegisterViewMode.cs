using System.ComponentModel.DataAnnotations;

namespace ElSayedHotel.ViewModel
{
    public class RegisterViewMode
    {
        public string Name { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
    }
}
