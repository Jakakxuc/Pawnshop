using PawnshopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.ViewModel
{
    public class CustomerViewModel
    {
        public string FullName { get; }
        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Guid CustomerUUID { get; set; }

        public CustomerViewModel()
        {
                
        }

        public CustomerViewModel(Customer customer)
        {
            FullName = $"{customer.Name} {customer.Surname}";
            PhoneNumber = customer.PhoneNumber;
            Name = customer.Name;
            Surname = customer.Surname;
            CustomerUUID = customer.UUID;
        }
    }
}
