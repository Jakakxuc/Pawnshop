using PawnshopApp.Entities;
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
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService()
        {
            _paymentRepository = ServiceProviderContainer.GetService<IPaymentRepository>();
        }

        public Task<Payment> AddAsync(Payment entity)
        {
            return _paymentRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid uuid)
        {
            return _paymentRepository.DeleteAsync(uuid);
        }

        public IEnumerable<Payment> GetAll()
        {
            return _paymentRepository.GetAll();
        }

        public Task<IEnumerable<Payment>> GetAllAsync()
        {
            return _paymentRepository.GetAllAsync();
        }

        public Task<Payment> GetByUUIDAsync(Guid uuid)
        {
            return _paymentRepository.GetByUUIDAsync(uuid);
        }

        public Task<Payment> UpdateAsync(Payment entity)
        {
            return _paymentRepository.UpdateAsync(entity);
        }
    }
}
