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
    public class PawnedItemService : IPawnedItemService
    {
        private readonly IPawnedItemRepository _pawnedItemRepository;

        public PawnedItemService()
        {
            _pawnedItemRepository = ServiceProviderContainer.GetService<IPawnedItemRepository>();
        }

        public Task<PawnedItem> AddAsync(PawnedItem entity)
        {
            return _pawnedItemRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid uuid)
        {
            return _pawnedItemRepository.DeleteAsync(uuid);
        }

        public IEnumerable<PawnedItem> GetAll()
        {
            return _pawnedItemRepository.GetAll();
        }

        public Task<IEnumerable<PawnedItem>> GetAllAsync()
        {
            return _pawnedItemRepository.GetAllAsync();
        }

        public Task<PawnedItem> GetByUUIDAsync(Guid uuid)
        {
            return _pawnedItemRepository.GetByUUIDAsync(uuid);
        }

        public Task<PawnedItem> UpdateAsync(PawnedItem entity)
        {
            return _pawnedItemRepository.UpdateAsync(entity);
        }
    }
}
