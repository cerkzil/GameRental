using GR.Domains.Enum;
using System;
using System.Collections.Generic;

namespace GR.Domains
{
    public class Platforms
    {
        public Guid Id { get; set; }
        public Platform Platform { get; set; }
        public Platforms()
        {
            //EF needs a parameterless constructor 
        }
        public Platforms(Platform platform)
        {
            Platform = platform;
        }
    }
}
