using AutoMapper;
using MyMusic.API.Ressources;
using MyMusic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusic.API.Mapping
{
    public class MappingProfile : Profile
    {
        // Domain (BD )vers Ressource
        public MappingProfile()
        {
            CreateMap<Music, MusicRessource>();
            CreateMap<Artist, ArtistRessource>();

            // Ressource vers Domain (BD)
            CreateMap<MusicRessource, Music>();
            CreateMap<ArtistRessource, Artist>();





        }

    }
}
