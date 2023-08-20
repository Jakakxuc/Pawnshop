using System;
using System.Collections.Generic;


namespace PawnshopApp.Entities
{
    /// <summary>
    /// Сущность "Документ"
    /// </summary>
    public class Document
    {
        public Document()
        {
            UUID = Guid.NewGuid();
        }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public long Number { get; set; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid UUID { get; set; }

        /// <summary>
        /// Дата создания документа
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Ссылка на клиента
        /// </summary>
        public Guid CustomerUUID { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }
    }
}

