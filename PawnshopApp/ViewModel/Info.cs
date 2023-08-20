using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.ViewModel
{
    public class Info
    {
        public decimal InitialAmount { get; set; }

        public decimal CurrentAmount { get; set; }

        public int SoldItems { get; set; }

        public decimal SoldProfitAmount { get; set; }

        public decimal LoanAmount { get; set; }

        public int PawnedItemsCount { get; set; }
    }
}
