using Microsoft.EntityFrameworkCore;
using MyMusic.Core.Models;
using MyMusic.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Data.Repositories
{
    public class MusicRepository : Repository<Music>, IMusicRepository
    {
        private MyMusicDbContext MyMusicDbContext
        {
            get { return Context as MyMusicDbContext; }
        }
        public MusicRepository(MyMusicDbContext myMusicDbContext) :base(myMusicDbContext)
        {

        }
        public async Task<IEnumerable<Music>> GetAllWithArtistAsync()
        {
            return await MyMusicDbContext.Musics
                .Include(m => m.Artist)
                .ToListAsync();
        }


        public async Task<Music> GetWithArtistByIdAsync(int id)
        {

            return await MyMusicDbContext.Musics
                .Include(m => m.Artist)
                .SingleOrDefaultAsync(m => m.ID == id);
        }

        public async Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId)
        {
            return await MyMusicDbContext.Musics
                .Include(m => m.Artist)
                .Where(m => m.ArtistId == artistId)
                .ToListAsync();                
        }

        async Task<IEnumerable<Music>> IMusicRepository.GetAllWithArtistAsync()
        {
            return await MyMusicDbContext.Musics
              .Include(m => m.Artist)
              .ToListAsync();
        }

        async Task<Music> IMusicRepository.GetWithArtistByIdAsync(int id)
        {
            return await MyMusicDbContext.Musics
               .Include(m => m.Artist)
               .SingleOrDefaultAsync(m => m.ID == id); ;
        }

        async Task<IEnumerable<Music>> IMusicRepository.GetAllWithArtistByArtistIdAsync(int artistId)
        {
            return await MyMusicDbContext.Musics
               .Include(m => m.Artist)
               .Where(m => m.ArtistId == artistId)
               .ToListAsync();
        }

    }
}
