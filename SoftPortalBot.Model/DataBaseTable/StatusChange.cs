using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Изменение статуса.
    /// </summary>
    [Table("StatusChange")]
    public class StatusChange
    {
        /// <summary>
        /// ID.
        /// </summary>
        [Column("Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// ID заявки.
        /// </summary>
        [Column("RequestId")]
        [Display(Name = "RequestId")]
        public int RequestId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа RequestId.
        /// </summary>
        [ForeignKey("RequestId")]
        public Request? Request { get; set; }

        /// <summary>
        /// Дата события. 
        /// </summary>
        [Column("EventTime")]
        [Display(Name = "EventTime")]
        public DateTime EventTime { get; set; }

        /// <summary>
        /// Название статуса.
        /// </summary>
        [Column("StatusName")]
        [Display(Name = "StatusName")]
        public string StatusName { get; set; }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public StatusChange()
        {

        }
    }
}
