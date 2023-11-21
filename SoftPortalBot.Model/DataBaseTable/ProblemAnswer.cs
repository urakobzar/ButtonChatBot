using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Вопрос по проблеме.
    /// </summary>
    [Table("ProblemAnswers")]
    public class ProblemAnswer
    {
        /// <summary>
        /// ID.
        /// </summary>
        [Column("Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// Ответ по проблеме.
        /// </summary>
        [Column("ProblemResponseId")]
        [Display(Name = "ProblemResponseId")]
        public int? ProblemResponseId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа ProblemResponseId.
        /// </summary>
        [ForeignKey("ProblemResponseId")]
        public ProblemResponse? ProblemResponse { get; set; }

        /// <summary>
        /// Текст ответа.
        /// </summary>
        [Column("AnswerText")]
        [Display(Name = "AnswerText")]
        public string AnswerText { get; set; }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public ProblemAnswer()
        {

        }
    }
}
