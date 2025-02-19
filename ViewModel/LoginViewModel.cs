using ElSayedHotel.Models;
using ElSayedHotel.ModelsMetaData;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ElSayedHotel.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

}
