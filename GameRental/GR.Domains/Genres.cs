using GR.Domains.Enum;
using System;

namespace GR.Domains
{
    public class Genres
    {
        public Guid Id { get; set; }
        public Genre Genre { get; set; }
        public Genres()
        {
        //EF needs a parameterless constructor
        }
        public Genres(Genre genre)
        {
            Genre = genre;
        }
    }
}
