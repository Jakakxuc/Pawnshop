using PawnshopApp.Entities;
using PawnshopApp.Services.Interfaces;
using PawnshopApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.ViewModel
{
    public class PaymentViewModel
    {
        public PaymentViewModel(Payment payment)
        {
            UUID = payment.UUID;
            LoanUUID = payment.LoanUUID;
            PaymentDate = payment.PaymentDate;
            PaymentAmount = payment.PaymentAmount;
        }

        public PaymentViewModel()
        {
                
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid UUID { get; set; }

        /// <summary>
        /// Ссылка на займ
        /// </summary>
        public Guid LoanUUID { get; set; }

        /// <summary>
        /// Сумма оплаты
        /// </summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>
        /// Дата оплаты
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Имя займа
        /// </summary>
        public string LoanName { get; set; }
    }
}
