using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Services.BusinessLogic.Interfaces
{
    public interface IAuthenticationService
    {
        Task CheckLoginAndPasswordAsync(string login, string password);

        Task RegisterUserAsync(string userName, string password);

        Task<List<int>> GetPasswordMaskAsync(string userName);
    }
}
