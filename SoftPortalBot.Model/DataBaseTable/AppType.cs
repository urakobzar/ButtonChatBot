using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Тип приложения.
    /// </summary>
    [Table("dictAppType")]
    public class AppType
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
        /// Описание.
        /// </summary>
        [Column("Description")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public AppType()
        {

        }

        /// <summary>
        /// Конструктор типа приложения.
        /// </summary>
        /// <param name="name">Название типа.</param>
        /// <param name="description">Описание типа.</param>
        public AppType(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
