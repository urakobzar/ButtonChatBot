using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Ответственная группа.
    /// </summary>
    [Table("ResponsibleGroups")]
    public class ResponsibleGroup
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
        public ResponsibleGroup()
        {

        }

        /// <summary>
        /// Конструктор ответственной группы.
        /// </summary>
        /// <param name="name">Название ответственной группы.</param>
        public ResponsibleGroup(string name)
        {
            Name = name;
        }
    }
}
