using GR.Domains.Enum;
using System.Collections.Generic;

namespace GR.Domains
{
    public class Game
    {
        public string Title { get; set; }
        public Platform Platform { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
