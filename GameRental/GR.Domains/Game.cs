using GR.Domains.Enum;
using System;
using System.Collections.Generic;

namespace GR.Domains
{
    public class Game
    {
        public Guid Id { get; set; }
        public Uri ImgLink { get; set; }
        public string Title { get; set; }
        public List<Genres> GenreList { get; set; }
        public List<Platforms> PlatformList { get; set; }
    }
}
