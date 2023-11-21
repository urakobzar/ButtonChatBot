using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Ответ по проблеме.
    /// </summary>
    [Table("ProblemResponses")]
    public class ProblemResponse
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
        /// Название.
        /// </summary>
        [Column("Name")]
        [Display(Name = "Name")]
        [MaxLength(25)]
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Column("Description")]
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Description { get; set; }

        /// <summary>
        /// Содержание.
        /// </summary>
        [Column("Content")]
        [Display(Name = "Content")]
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string Content { get; set; }

        /// <summary>
        /// Конструктор вопроса базы знаний.
        /// </summary>
        /// <param name="applicationId">ID приложения.</param>
        /// <param name="name">Название.</param>
        /// <param name="description">Описание.</param>
        /// <param name="content">Содержание.</param>
        public ProblemResponse(int applicationId, string name, string description,string content)
        {
            ApplicationId = applicationId;
            Name = name;
            Description = description;
            Content = content;
        }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public ProblemResponse()
        {

        }
    }
}
