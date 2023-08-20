using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Services.BusinessLogic.Interfaces
{
    public interface IDocumentNumberService
    {
        long GetNextNumber();
    }
}
