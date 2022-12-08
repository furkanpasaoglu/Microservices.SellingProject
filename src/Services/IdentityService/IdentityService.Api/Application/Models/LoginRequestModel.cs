﻿using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Application.Models
{
    public class LoginRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
