using PawnshopApp.Repository.Interfaces;
using PawnshopApp.Services.BusinessLogic.Interfaces;
using PawnshopApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Services.BusinessLogic
{
    public class DocumentNumberService : IDocumentNumberService
    {
        private readonly IDocumentNumberRepository _documentNumberRepository;

        public DocumentNumberService()
        {
            _documentNumberRepository = ServiceProviderContainer.GetService<IDocumentNumberRepository>();
        }

        public long GetNextNumber()
        {
            long numberToReturn = _documentNumberRepository.GetCurrentNumber();
            _documentNumberRepository.UpdateLastDocumentNumber(numberToReturn);
            return numberToReturn;
        }
    }
}
