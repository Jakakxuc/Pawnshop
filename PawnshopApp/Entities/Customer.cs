using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnshopApp.Entities
{
    /// <summary>
    /// Сущность клиент
    /// </summary>
    public class Customer
    {

        public Customer()
        {
            UUID = Guid.NewGuid();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UUID { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Surname { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }
    }
}
