 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusic.API.Ressources
{
    public class MusicRessource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ArtistRessource Artist { get; set; }

    }
}
