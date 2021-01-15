using MyMusic.Core;
using MyMusic.Core.Models;
using MyMusic.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Services.Services
{
    public class UserService : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            return await _unitOfWork.Users.Authenticate(username, password);
            
        }

        public async Task<User> Create(User user, string password)
        {
             await _unitOfWork.Users.Create(user, password);
            await _unitOfWork.CommitAsync();
            return user;

        }

        public void Delete(int id)
        {
            _unitOfWork.Users.Delete(id);
            _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _unitOfWork.Users.GetAllUSerAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _unitOfWork.Users.GetWithUserByIdAsync(id);
        }

        public void Update(User user, string password = null)
        {
           _unitOfWork.Users.Update(user, password);
            _unitOfWork.CommitAsync();
        }
    }
}
