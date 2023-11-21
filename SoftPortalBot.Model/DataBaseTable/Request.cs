using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Заявка.
    /// </summary>
    public class Request
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
        /// ID создателя.
        /// </summary>
        [Column("CreatorId")]
        [Display(Name = "CreatorId")]
        public int CreatorId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа CreatorId.
        /// </summary>
        [ForeignKey("CreatorId")]
        public User? Creator { get; set; }

        /// <summary>
        /// ID исполнителя.
        /// </summary>
        [Column("ExecutorId")]
        [Display(Name = "ExecutorId")]
        public int? ExecutorId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа ExecutorId.
        /// </summary>
        [ForeignKey("ExecutorId")]
        public User? Executor { get; set; }

        /// <summary>
        /// ID причины заявки.
        /// </summary>
        [Column("RequestReasonId")]
        [Display(Name = "RequestReasonId")]
        public int RequestReasonId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа RequestReasonId.
        /// </summary>
        [ForeignKey("RequestReasonId")]
        public RequestReason? RequestReason { get; set; }

        /// <summary>
        /// ID статуса заявки.
        /// </summary>
        [Column("StatusId")]
        [Display(Name = "StatusId")]
        public int StatusId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа StatusId.
        /// </summary>
        [ForeignKey("StatusId")]
        public RequestStatus? RequestStatus { get; set; }

        /// <summary>
        /// Дата создания заявки.
        /// </summary>
        [Column("CreateTime")]
        [Display(Name = "CreateTime")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Описание проблемы.
        /// </summary>
        [Column("ProblemDescription")]
        [Display(Name = "ProblemDescription")]
        [Required(ErrorMessage = "Поле не может быть пустым!")]
        public string ProblemDescription { get; set; }

        /// <summary>
        /// Ссылка на вложения.
        /// </summary>
        [Column("AttachmentsLink")]
        [Display(Name = "AttachmentsLink")]
        public string? AttachmentsLink { get; set; }

        /// <summary>
        /// Номер компьютер.
        /// </summary>
        [Column("ComputerNumber")]
        [Display(Name = "ComputerNumber")]
        public int? ComputerNumber { get; set; }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public Request()
        {

        }
    }
}
