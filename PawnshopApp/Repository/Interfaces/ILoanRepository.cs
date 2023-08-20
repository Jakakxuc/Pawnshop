﻿using PawnshopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Repository.Interfaces
{
    public interface ILoanRepository : IRepository<Loan>
    {
        Loan GetByUUID(Guid uuid);
    }
}
