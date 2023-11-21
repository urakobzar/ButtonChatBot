using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Статус заявки.
    /// </summary>
    [Table("dictRequestStatus")]
    public class RequestStatus
    {
        /// <summary>
        /// ID.
        /// </summary>
        [Column("Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        [Column("Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Конструктор статуса заявки.
        /// </summary>
        /// <param name="name">Название статуса заявки.</param>
        public RequestStatus(string name)
        {
            Name = name;
        }
    }
}
