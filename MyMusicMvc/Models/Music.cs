using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicMvc.Models
{
    public class Music
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
