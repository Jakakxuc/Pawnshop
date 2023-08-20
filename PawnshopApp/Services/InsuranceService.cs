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
    public class InsuranceService : IInsuranceService
    {
        private readonly IInsuranceRepository _insuranceRepository;

        public InsuranceService()
        {
            _insuranceRepository = ServiceProviderContainer.GetService<IInsuranceRepository>();
        }

        public Task<Insurance> AddAsync(Insurance entity)
        {
            return _insuranceRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid uuid)
        {
            return _insuranceRepository.DeleteAsync(uuid);
        }

        public IEnumerable<Insurance> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Insurance>> GetAllAsync()
        {
            return _insuranceRepository.GetAllAsync();
        }

        public Task<Insurance> GetByUUIDAsync(Guid uuid)
        {
            return _insuranceRepository.GetByUUIDAsync(uuid);
        }

        public Task<Insurance> UpdateAsync(Insurance entity)
        {
            return _insuranceRepository.UpdateAsync(entity);
        }
    }
}
