using PawnshopApp.Entities;
using PawnshopApp.Repository.Interfaces;
using PawnshopApp.Services.BusinessLogic.Interfaces;
using PawnshopApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Services.BusinessLogic
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private Dictionary<string, List<int>> _passwordMask = new Dictionary<string, List<int>>();

        public AuthenticationService()
        {
            _userRepository = ServiceProviderContainer.GetService<IUserRepository>();
        }

        public async Task CheckLoginAndPasswordAsync(string login, string password)
        {
            User user = await _userRepository.GetByLoginAsync(login);

            if (user == null)
                throw new Exception("Неверный логин или пароль");

            string originalPassword = user.PasswordHash;
            if(!_passwordMask.TryGetValue(login, out List<int> result))
                throw new Exception("Получите маску пароля");

            StringBuilder originalPasswordByMask = new();
            foreach(var position in result)
            {
                originalPasswordByMask.Append(originalPassword[position]);
            }

            if (!string.Equals(originalPasswordByMask.ToString(), password))
            {
                throw new Exception("Неверный логин или пароль");
            }

            _passwordMask.Remove(login);
        }

        public async Task<List<int>> GetPasswordMaskAsync(string userName)
        {
            User user = await _userRepository.GetByLoginAsync(userName);

            if (user == null)
                throw new Exception("Неверный логин");

            string password = user.PasswordHash;

            var random = new Random();
            int positionsAmount = random.Next(1, password.Length-1);
            List<int> positions = GenerateUnuqiePostions(positionsAmount, password.Length - 1);
            _passwordMask[userName] = positions;
            return positions;

        }

        private List<int> GenerateUnuqiePostions(int positionAmount, int passwordLength)
        {
            List<int> result = new();

            while(positionAmount > 0)
            {

                var random = new Random();
                int position = random.Next(random.Next(0, passwordLength));
                if (!result.Contains(position))
                {
                    result.Add(position);
                    positionAmount--;
                }
            }

            return result;
        }

        public async Task RegisterUserAsync(string userName, string password)
        {
            User user = new User()
            {
                PasswordHash = password,
                Username = userName
            };

            await _userRepository.AddAsync(user);
        }

        private string ComputeHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
