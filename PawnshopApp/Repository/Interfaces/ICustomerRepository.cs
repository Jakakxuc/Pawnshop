using PawnshopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Repository.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByUUID(Guid uuid);
    }
}
