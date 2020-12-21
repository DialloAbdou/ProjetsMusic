using MongoDB.Bson;
using MongoDB.Driver;
using MyMusic.Core.Models;
using MyMusic.Core.Repositories;
using MyMusic.Data.MongoDB.Setting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Data.MongoDB.Repository
{
    public class ComposerRepository : IComposerRepository
    {
        private readonly IDatabaseSettings _context;
        public ComposerRepository(IDatabaseSettings context)
        {
            _context = context;
        }
        public async Task<Composer> Create(Composer composer)
        {
            await _context.Composers.InsertOneAsync(composer);
            return composer;
        }

        public  async Task<bool> Delete(string id)
        {
            ObjectId idMongo = new ObjectId(id);
            FilterDefinition<Composer> filter = Builders<Composer>.Filter.Eq(m => m.Id, idMongo);
            DeleteResult deleteResult = await _context
                                          .Composers.
                                          DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;

        }

        public async Task<IEnumerable<Composer>> GetAllComposers()
        {
            return await _context
                   .Composers
                   .Find(_ => true)
                   .ToListAsync();
        }

        public async Task<Composer> GetComposerById(string id)
        {
            ObjectId idMongo = new ObjectId(id);
            FilterDefinition<Composer> filter = Builders<Composer>.Filter.Eq(m => m.Id, idMongo);
            return await _context
                .Composers
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public void update(string id, Composer composer)
        {
            ObjectId idMongo = new ObjectId(id);
            FilterDefinition<Composer> filter = Builders<Composer>.Filter.Eq(m => m.Id, idMongo);
            var update = Builders<Composer>.Update.Set("FirstName", composer.FirstName).Set("LastName", composer.LastName);
            _context.Composers.FindOneAndUpdate(filter, update);
        }
    }
}
