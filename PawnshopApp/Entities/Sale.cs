using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Entities
{
    /// <summary>
    /// Сущность продажи
    /// </summary>
    public class Sale
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid UUID { get; set; } = new Guid();

        /// <summary>
        /// Ссылка на залоговый предмет
        /// </summary>
        public Guid PawnedItemUUID { get; set; }
        
        /// <summary>
        /// Цена продажи
        /// </summary>
        public decimal SaleAmount { get; set; }

        /// <summary>
        /// Дата продажи
        /// </summary>
        public DateTime SaleDate { get; set; }
    }
}
