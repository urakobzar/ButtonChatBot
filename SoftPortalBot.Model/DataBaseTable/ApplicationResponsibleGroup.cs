using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Связь между приложением и ответственной группой.
    /// </summary>
    [Table("ApplicationResponsibleGroup")]
    public class ApplicationResponsibleGroup
    {
        /// <summary>
        /// ID.
        /// </summary>
        [Column("Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// ID приложения.
        /// </summary>
        [Column("ApplicationId")]
        [Display(Name = "ApplicationId")]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа ApplicationId.
        /// </summary>
        [ForeignKey("ApplicationId")]
        public Application? Application { get; set; }

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
        /// Пустой конструктор.
        /// </summary>
        public ApplicationResponsibleGroup()
        {

        }

        /// <summary>
        /// Конструктор связи приложения и ответственной группы.
        /// </summary>
        /// <param name="applicationId">ID приложения.</param>
        /// <param name="responsibleGroupId">ID ответственной группы.</param>
        public ApplicationResponsibleGroup(int applicationId, int responsibleGroupId)
        {
            ApplicationId = applicationId;
            ResponsibleGroupId = responsibleGroupId;
        }
    }
}
