using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Функции группы.
    /// </summary>
    [Table("dictGroupFunction")]
    public class GroupFunction
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
        /// Пустой конструктор.
        /// </summary>
        public GroupFunction()
        {

        }
    }
}
