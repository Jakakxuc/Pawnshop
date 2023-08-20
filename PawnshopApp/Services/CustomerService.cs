using PawnshopApp.Entities;
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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = ServiceProviderContainer.GetService<ICustomerRepository>();
        }

        public Task<Customer> AddAsync(Customer entity)
        {
            return _customerRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid uuid)
        {
            return _customerRepository.DeleteAsync(uuid);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            return _customerRepository.GetAllAsync();
        }

        public Customer GetByUUID(Guid uuid)
        {
            return _customerRepository.GetByUUID(uuid);
        }

        public Task<Customer> GetByUUIDAsync(Guid uuid)
        {
            return _customerRepository.GetByUUIDAsync(uuid);
        }

        public Task<Customer> UpdateAsync(Customer entity)
        {
            return _customerRepository.UpdateAsync(entity);
        }
    }
}
