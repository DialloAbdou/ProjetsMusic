using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyMusic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusic.Data.MongoDB.Setting
{
    public class DataBaseSetting : IDatabaseSettings
    {
        private readonly IMongoDatabase _db;
        public DataBaseSetting(IOptions<Settings>options, IMongoClient client)
        {
            _db = client.GetDatabase(options.Value.DataBase);
        }
        public IMongoCollection<Composer> Composers => _db.GetCollection<Composer>("Composers");
    }
}
