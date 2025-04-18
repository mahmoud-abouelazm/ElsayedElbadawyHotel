﻿using ElSayedHotel.ModelsMetaData;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ElSayedHotel.ViewModel
{
    [ModelMetadataType(typeof(PasswordMetaData))]
    public class SignUpViewModel
    {
        [Required]
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string Address { get; set; }
        public string password { get; set; }
        [DataType(DataType.Password)]
        [Compare("password" , ErrorMessage ="Please ewite password correctly")]
        public string confirmPassword { get; set; }
        [Range(0 , 2)] // user , owner , admin
        public int accountType { get; set; } // Assuming accountType is an integer

    }
}
