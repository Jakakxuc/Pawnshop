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
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService()
        {
            _documentRepository = ServiceProviderContainer.GetService<IDocumentRepository>();
        }

        public Task<Document> AddAsync(Document entity)
        {
            return _documentRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid uuid)
        {
            return _documentRepository.DeleteAsync(uuid);
        }

        public IEnumerable<Document> GetAll()
        {
            return _documentRepository.GetAll();
        }

        public Task<IEnumerable<Document>> GetAllAsync()
        {
            return _documentRepository.GetAllAsync();
        }

        public Document GetByUUID(Guid uuid)
        {
            return _documentRepository.GetByUUID(uuid);
        }

        public Task<Document> GetByUUIDAsync(Guid uuid)
        {
            return _documentRepository.GetByUUIDAsync(uuid);
        }

        public Task<Document> UpdateAsync(Document entity)
        {
            return _documentRepository.UpdateAsync(entity);
        }
    }
}
