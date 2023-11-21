using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Приложение.
    /// </summary>
    [Table("Applications")]
    public class Application
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
        /// Версия.
        /// </summary>
        [Column("Version")]
        [Display(Name = "Version")]
        public string? Version { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Column("Description")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        /// <summary>
        /// Краткое описание.
        /// </summary>
        [Column("ShortDescription")]
        [Display(Name = "ShortDescription")]
        public string? ShortDescription { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        [Column("Created")]
        [Display(Name = "Created")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// Дата обновления.
        /// </summary>
        [Column("Updated")]
        [Display(Name = "Updated")]
        public DateTime? Updated { get; set; }

        /// <summary>
        /// Аббревиатура.
        /// </summary>
        [Column("Abbreviation")]
        [Display(Name = "Abbreviation")]
        public string? Abbreviation { get; set; }

        /// <summary>
        /// Системные требования.
        /// </summary>
        [Column("Requirements")]
        [Display(Name = "Requirements")]
        public string? Requirements { get; set; }

        /// <summary>
        /// Количество загрузок.
        /// </summary>
        [Column("DownloadCount")]
        [Display(Name = "DownloadCount")]
        public int? DownloadCount { get; set; }

        /// <summary>
        /// ID типа приложения.
        /// </summary>
        [Column("AppTypeId")]
        [Display(Name = "AppTypeId")]
        public int AppTypeId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа AppTypeId.
        /// </summary>
        [ForeignKey("AppTypeId")]
        public AppType AppType { get; set; }

        /// <summary>
        /// Директория файла установки.
        /// </summary>
        [Column("FilePath")]
        [Display(Name = "FilePath")]
        public string? FilePath { get; set; }

        /// <summary>
        /// Директория документации.
        /// </summary>
        [Column("DocumentationPath")]
        [Display(Name = "DocumentationPath")]
        public string? DocumentationPath { get; set; }

        /// <summary>
        /// История изменения.
        /// </summary>
        [Column("ChangeHistory")]
        [Display(Name = "ChangeHistory")]
        public string? ChangeHistory { get; set; }

        /// <summary>
        /// Рекомендации к установке.
        /// </summary>
        [Column("InstallationRecommendations")]
        [Display(Name = "InstallationRecommendations")]
        public string? InstallationRecommendations { get; set; }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public Application()
        {

        }

        /// <summary>
        /// Конструктор приложения.
        /// </summary>
        /// <param name="name">Название приложения.</param>
        /// <param name="appTypeId">Тип приложения.</param>
        public Application(string name, int appTypeId)
        {
            Name = name;
            AppTypeId = appTypeId;
        }
    }
}
