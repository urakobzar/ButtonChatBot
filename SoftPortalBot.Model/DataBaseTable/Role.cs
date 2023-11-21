using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Роль пользователя.
    /// </summary>
    [Table("dictRoles")]
    public class Role
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
        public Role()
        {

        }

        /// <summary>
        /// Конструктор роли.
        /// </summary>
        /// <param name="name">Название роли.</param>
        public Role(string name)
        {
            Name = name;
        }
    }
}
