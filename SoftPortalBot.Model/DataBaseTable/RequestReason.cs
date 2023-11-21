using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Причина обращения.
    /// </summary>
    [Table("dictRequestReason")]
    public class RequestReason
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
        /// Конструктор причины обращения.
        /// </summary>
        /// <param name="name">Название причины.</param>
        public RequestReason(string name)
        {
            Name = name;
        }
    }
}
