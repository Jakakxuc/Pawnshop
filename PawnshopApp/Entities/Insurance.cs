using System;

namespace PawnshopApp.Entities
{
    /// <summary>
    /// Сущность Страховка
    /// </summary>
    public class Insurance
    {
        public Insurance()
        {
            UUID = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid UUID { get; set; }

        /// <summary>
        /// Ссылка на заложенный предмет
        /// </summary>
        public Guid PawnedItemUUID { get; set; }

        /// <summary>
        /// Размер страховки
        /// </summary>
        public decimal InsuranceAmount { get; set; }

        /// <summary>
        /// Дата страхования
        /// </summary>
        public DateTime InsuranceDate { get; set; }
    }

}
