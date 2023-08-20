using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Entities
{
    /// <summary>
    /// Сущность "Займ"
    /// </summary>
    public class Loan
    {
        public Loan()
        {
            UUID = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
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
        public LoanStatus Status { get; set; }

        /// <summary>
        /// Ссылка на документ-основание
        /// </summary>
        public Guid DocumentUUID { get; set; }
    }

    /// <summary>
    /// Статус займа
    /// </summary>
    public enum LoanStatus
    {
        Active,
        Closed,
        Defaulted
    }
}
