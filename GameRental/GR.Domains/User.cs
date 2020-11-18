using System.Collections.Generic;
using GR.Domains.Enum;

namespace GR.Domains
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<Platform> Platforms {get; set;}
    }
}
