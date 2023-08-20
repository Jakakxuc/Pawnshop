using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<T> GetByUUIDAsync(Guid uuid);

        Task<T> AddAsync(T entity);

        Task DeleteAsync(Guid uuid);

        Task<T> UpdateAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> GetAll();
    }
}
