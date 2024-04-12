﻿using Microsoft.AspNetCore.Identity;

namespace COMP2139_Labs.Areas.ProjectManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserNameChangeLimit { get; set; } = 10;
        public byte[]? ProfilePic { get; set; }

    }
}
