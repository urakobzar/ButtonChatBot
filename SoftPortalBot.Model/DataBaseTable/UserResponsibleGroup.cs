using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Связь между ответственной группой и пользователем.
    /// </summary>
    [Table("UserResponsibleGroup")]
    public class UserResponsibleGroup
    {
        /// <summary>
        /// ID.
        /// </summary>
        [Column("Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// ID пользователя.
        /// </summary>
        [Column("UserId")]
        [Display(Name = "UserId")]
        public int UserId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа UserId.
        /// </summary>
        [ForeignKey("UserId")]
        public User? User { get; set; }

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
        public UserResponsibleGroup()
        {

        }

        /// <summary>
        /// Конструктор связи между ответственной группой и пользователем.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        /// <param name="responsibleGroupId">ID ответственной группы.</param>
        public UserResponsibleGroup(int userId, int responsibleGroupId)
        {
            UserId = userId;
            ResponsibleGroupId = responsibleGroupId;
        }
    }
}
