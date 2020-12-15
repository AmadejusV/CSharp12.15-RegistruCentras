using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistruCentras.Domains
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
