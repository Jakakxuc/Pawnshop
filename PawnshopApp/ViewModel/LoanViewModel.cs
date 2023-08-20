using DevExpress.Mvvm.POCO;
using PawnshopApp.Entities;
using PawnshopApp.Pages;
using PawnshopApp.Services.Interfaces;
using PawnshopApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.ViewModel
{
    public class LoanViewModel
    {
        /// <summary>
        /// Заложенные предметы
        /// </summary>
        public List<PawnedItemViewModel> PawnedItems { get; set; } = new();

        /// <summary>
        /// Платежи
        /// </summary>
        public List<PaymentViewModel> Payments { get; set; } = new();


        public Guid UUID { get; set; }

        /// <summary>
        /// Размер займа
        /// </summary>
        public decimal LoanAmount { get; set; }

        /// <summary>
        /// Процентная ставка
        /// </summary>
        public decimal InterestRate { get; set; }

        /// <summary>
        /// Дата открытия займа
        /// </summary>
        public DateTime LoanDate { get; set; }

        /// <summary>
        /// Статус займа
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Ссылка на документ-основание
        /// </summary>
        public Guid DocumentUUID { get; set; }

        public string Name { get; set; }

        public LoanViewModel(Loan loan)
        {
            LoanAmount = loan.LoanAmount;
            InterestRate = loan.InterestRate;
            DocumentUUID = loan.DocumentUUID;
            Status = loan.Status.ToString();
            LoanDate = loan.LoanDate;
            UUID = loan.UUID;

            LoadData(loan);
        }

        private void LoadData(Loan loan)
        {
            List<PawnedItem> pawnedItems = ServiceProviderContainer.GetService<IPawnedItemService>().GetAll().Where(x => x.LoanUUID == loan.UUID).ToList();
            foreach (var item in pawnedItems)
            {
                PawnedItemViewModel paymentViewModel = new PawnedItemViewModel(item);
                PawnedItems.Add(paymentViewModel);
            }

            List<Payment> payments = ServiceProviderContainer.GetService<IPaymentService>().GetAll().Where(x => x.LoanUUID == loan.UUID).ToList();
            foreach (var payment in payments)
            {
                PaymentViewModel paymentViewModel = new PaymentViewModel(payment);
                Payments.Add(paymentViewModel);
            }

            Document document = ServiceProviderContainer.GetService<IDocumentService>().GetByUUID(loan.DocumentUUID);
            Name = $"Займ по документу {document.Number}";
        }
    }
}
