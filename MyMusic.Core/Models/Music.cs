using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusic.Core.Models
{
    public class Music
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
