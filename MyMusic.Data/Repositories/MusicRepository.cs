using MyMusic.Core.Models;
using MyMusic.Core.Repositories;
using System;
using System.Collections.Generic;
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
        public Task<IEnumerable<Music>> GetAllWithArtistAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId)
        {
            throw new NotImplementedException();
        }

        public Task<Music> GetWithArtistByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
