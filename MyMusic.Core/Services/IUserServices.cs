using MyMusic.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Core.Services
{
    public interface IUserServices
    {
        Task<User> Authenticate(string username, string password);
        Task<User> Create(User user, string password);
        Task<IEnumerable< User>>GetAll();
        Task<User> GetById(int id);
       void Update(User user, string password = null);
       void Delete(int id);

    }
}
