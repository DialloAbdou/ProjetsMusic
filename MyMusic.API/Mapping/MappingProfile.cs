﻿using AutoMapper;
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
        //===Une Classe qui permet de mapper des Champs dont l'utilisateur a besoin
        public MappingProfile()
        {
            // Domain (BD )vers Ressource
            CreateMap<Music, MusicRessource>();
            CreateMap<Artist, ArtistRessource>();
            CreateMap<Music, SaveMusicRessource>();

            // Ressource vers Domain (BD)
            CreateMap<MusicRessource, Music>();
            CreateMap<ArtistRessource, Artist>();
            CreateMap<SaveMusicRessource, Music>();






        }

    }
}
