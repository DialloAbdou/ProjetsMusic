using MyMusic.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Core
{
    public interface IUnitOfWork:IDisposable
    {
        IArtistRepository Artist { get; }
        IMusicRepository Music { get; }
        Task<int> CommitAsync();
    }
}
