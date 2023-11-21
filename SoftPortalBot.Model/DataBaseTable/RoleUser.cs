using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Связь роли и пользователя.
    /// </summary>
    [Table("RoleUser")]
    public class RoleUser
    {
        /// <summary>
        /// ID.
        /// </summary>
        [Column("Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// ID роли.
        /// </summary>
        [Column("RoleId")]
        [Display(Name = "RoleId")]
        public int RoleId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа RoleId.
        /// </summary>
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

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
        /// ID инициатора роли.
        /// </summary>
        [Column("RoleInitiatorId")]
        [Display(Name = "RoleInitiatorId")]
        public int? RoleInitiatorId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа RoleInitiatorId.
        /// </summary>
        [ForeignKey("RoleInitiatorId")]
        public User? RoleInitiator { get; set; }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public RoleUser()
        {

        }

        /// <summary>
        /// Конструктор связи роли и пользователя.
        /// </summary>
        /// <param name="userId">ID пользователя.</param>
        /// <param name="roleId">ID роли.</param>
        public RoleUser(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
