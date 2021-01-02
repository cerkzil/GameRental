using System.Collections.Generic;
using GR.Domains.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;

namespace GR.Domains
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public Platform Platform { get; set; }
    }
}
