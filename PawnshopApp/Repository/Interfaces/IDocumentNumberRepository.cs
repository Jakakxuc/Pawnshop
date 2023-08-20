using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Repository.Interfaces
{
    public interface IDocumentNumberRepository
    {
        long GetCurrentNumber();

        void UpdateLastDocumentNumber(long lastNumber);
    }
}
