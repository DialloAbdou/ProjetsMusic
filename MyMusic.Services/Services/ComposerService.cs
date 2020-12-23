using MyMusic.Core.Models;
using MyMusic.Core.Repositories;
using MyMusic.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Services.Services
{
    public class ComposerService : IComposerService
    {
        private readonly IComposerRepository _context;

        public ComposerService(IComposerRepository context)
        {
            _context = context;
        }

        public async Task<Composer> Create(Composer composer)
        {
            return await _context.Create(composer);
        }

        public async Task<bool> Delete(string id)
        {
            return await _context.Delete(id);
        }
     

        public async Task<IEnumerable<Composer>> GetAllComposers()
        {
            return await _context.GetAllComposers();
        }

        public async Task<Composer> GetComposerById(string id)
        {
            return await _context.GetComposerById(id);
        }

        public void update(string id, Composer composer)
        {
            _context.update(id, composer);

        }
    }
}
