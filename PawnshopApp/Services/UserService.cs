using PawnshopApp.Entities;
using PawnshopApp.Repository;
using PawnshopApp.Repository.Interfaces;
using PawnshopApp.Services.Interfaces;
using PawnshopApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = ServiceProviderContainer.GetService<IUserRepository>();
        }

        public Task<User> AddAsync(User entity)
        {
            return _userRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid uuid)
        {
            return _userRepository.DeleteAsync(uuid);
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<User> GetByLoginAsync(string login)
        {
            return _userRepository.GetByLoginAsync(login);
        }

        public Task<User> GetByUUIDAsync(Guid uuid)
        {
            return _userRepository.GetByUUIDAsync(uuid);
        }

        public Task<User> UpdateAsync(User entity)
        {
            return _userRepository.UpdateAsync(entity);
        }
    }
}
