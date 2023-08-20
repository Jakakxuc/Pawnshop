using PawnshopApp.Entities;
using PawnshopApp.Pages;
using PawnshopApp.Services.Interfaces;
using PawnshopApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.ViewModel
{
    public class DocumentViewModel
    {
        public List<LoanViewModel> LoansViewModel { get; set; }

        public CustomerViewModel Customer { get; set; }

        public List<PaymentViewModel> PaymentViewModels { get; set; }

        public List<PawnedItemViewModel> PawnedItems { get; set; }

        public Guid UUID { get; set; }

        /// <summary>
        /// Ссылка на клиента
        /// </summary>
        public Guid CustomerUUID { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public long Number { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Сумма займа
        /// </summary>
        public decimal PawnedAmount { get; set; }

        public DocumentViewModel()
        {
            
        }

        public DocumentViewModel(Document document)
        {
            UUID = document.UUID;
            Name = document.Name;
            Number = document.Number;
            CustomerUUID = document.CustomerUUID;

            LoadData(document);
        }

        private void LoadData(Document document)
        {
            Customer customer  = ServiceProviderContainer.GetService<ICustomerService>().GetByUUID(document.CustomerUUID);
            Customer = new CustomerViewModel(customer);
            var loans = ServiceProviderContainer.GetService<ILoanService>().GetAll();
            LoansViewModel = ServiceProviderContainer.GetService<ILoanService>().GetAll().Where(l => l.DocumentUUID == document.UUID)
                                                                                        .Select(l => new LoanViewModel(l))
                                                                                        .ToList();

            PaymentViewModels = LoansViewModel.SelectMany(l => l.Payments).ToList();
            PawnedItems = LoansViewModel.SelectMany(l => l.PawnedItems).ToList();

            PawnedAmount = PawnedItems.Sum(pi => pi.EstimatedValue);
        }
    }
}
