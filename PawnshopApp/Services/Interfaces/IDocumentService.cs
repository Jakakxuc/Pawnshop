﻿using PawnshopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Services.Interfaces
{
    public interface IDocumentService : IService<Document>
    {
        Document GetByUUID(Guid uuid);
    }
}
