﻿using Microsoft.AspNetCore.Identity;

namespace ForestWebUi.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
    }
}
