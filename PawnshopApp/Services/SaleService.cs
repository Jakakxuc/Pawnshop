using PawnshopApp.Entities;
using PawnshopApp.Repository;
using PawnshopApp.Repository.Interfaces;
using PawnshopApp.Services.Interfaces;
using PawnshopApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService()
        {
            _saleRepository = ServiceProviderContainer.GetService<ISaleRepository>();
        }

        public Task<Sale> AddAsync(Sale entity)
        {
            return _saleRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid uuid)
        {
           return _saleRepository.DeleteAsync(uuid);
        }

        public IEnumerable<Sale> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Sale>> GetAllAsync()
        {
            return _saleRepository.GetAllAsync();
        }

        public Task<Sale> GetByUUIDAsync(Guid uuid)
        {
            return _saleRepository.GetByUUIDAsync(uuid);
        }

        public Task<Sale> UpdateAsync(Sale entity)
        {
            return _saleRepository.UpdateAsync(entity);
        }
    }
}
