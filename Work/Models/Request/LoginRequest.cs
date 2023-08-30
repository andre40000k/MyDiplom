﻿using System.ComponentModel.DataAnnotations;

namespace LoginComponent.Models.Request
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
