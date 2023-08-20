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
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService()
        {
            _loanRepository = ServiceProviderContainer.GetService<ILoanRepository>();
        }

        public Task<Loan> AddAsync(Loan entity)
        {
            return _loanRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid uuid)
        {
            return _loanRepository.DeleteAsync(uuid);
        }

        public IEnumerable<Loan> GetAll()
        {
            return _loanRepository.GetAll();
        }

        public Task<IEnumerable<Loan>> GetAllAsync()
        {
            return _loanRepository.GetAllAsync();
        }

        public Loan GetByUUID(Guid uuid)
        {
            return _loanRepository.GetByUUID(uuid);
        }

        public Task<Loan> GetByUUIDAsync(Guid uuid)
        {
            return _loanRepository.GetByUUIDAsync(uuid);
        }

        public Task<Loan> UpdateAsync(Loan entity)
        {
            return _loanRepository.UpdateAsync(entity);
        }
    }
}
