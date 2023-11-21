using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Связь между функцией и ответственной группой.
    /// </summary>
    [Table("ResponsibleGroupFunction")]
    public class ResponsibleGroupFunction
    {
        /// <summary>
        /// ID.
        /// </summary>
        [Column("Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// ID ответственной группы.
        /// </summary>
        [Column("ResponsibleGroupId")]
        [Display(Name = "ResponsibleGroupId")]
        public int ResponsibleGroupId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа ResponsibleGroupId.
        /// </summary>
        [ForeignKey("ResponsibleGroupId")]
        public ResponsibleGroup? ResponsibleGroup { get; set; }

        /// <summary>
        /// ID функции группы.
        /// </summary>
        [Column("GroupFunctionId")]
        [Display(Name = "GroupFunctionId")]
        public int GroupFunctionId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа GroupFunctionId.
        /// </summary>
        [ForeignKey("GroupFunctionId")]
        public GroupFunction? GroupFunction { get; set; }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public ResponsibleGroupFunction()
        {

        }
    }
}
