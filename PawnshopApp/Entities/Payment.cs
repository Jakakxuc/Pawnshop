using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Entities
{
    /// <summary>
    /// Сущность "Оплата"
    /// </summary>
    public class Payment
    {
        public Payment()
        {
            UUID = Guid.NewGuid();
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

    }
}
