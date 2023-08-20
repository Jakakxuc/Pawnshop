using PawnshopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.ViewModel
{
    public class PawnedItemViewModel
    {

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
        public string Sold { get; set; }

        public PawnedItemViewModel(PawnedItem pawnedItem)
        {
            LoanUUID = pawnedItem.LoanUUID;
            Description = pawnedItem.Description;
            EstimatedValue = pawnedItem.EstimatedValue;
            PawnDate = pawnedItem.PawnDate;
            ExpiryDate = pawnedItem.ExpiryDate;
            Sold = pawnedItem.IsSold ? "Продан" : "Не продан";
            UUID = pawnedItem.UUID;
        }

        public PawnedItemViewModel()
        {
                
        }
    }
}
