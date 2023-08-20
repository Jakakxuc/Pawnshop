using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Entities
{
    /// <summary>
    /// Сущность "Заложенный предмет"
    /// </summary>
    public class PawnedItem
    {
        public PawnedItem()
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
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Оценочная стоимость
        /// </summary>
        public decimal EstimatedValue { get; set; }

        /// <summary>
        /// Дата залога
        /// </summary>
        public DateTime PawnDate { get; set; }

        /// <summary>
        /// Дата просрочки
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Продан
        /// </summary>
        public bool IsSold { get; set; }
    }
}
