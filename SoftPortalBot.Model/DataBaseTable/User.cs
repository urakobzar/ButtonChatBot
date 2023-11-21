using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    [Table("Users")]
    public class User
    {
        /// <summary>
        /// ID.
        /// </summary>
        [Column("Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        [Column("Login")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Column("Surname")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        [Column("Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        [Column("Patronymic")]
        [Display(Name = "Patronymic")]
        public string Patronymic { get; set; }

        /// <summary>
        /// SID.
        /// </summary>
        [Column("Sid")]
        [Display(Name = "Sid")]
        public string? Sid { get; set; }

        /// <summary>
        /// ID департамента.
        /// </summary>
        [Column("DepartmentId")]
        [Display(Name = "DepartmentId")]
        public int? DepartmentId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа DepartmentId.
        /// </summary>
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        /// <summary>
        /// Дата последней активности.
        /// </summary>
        [Column("LastActivity")]
        [Display(Name = "LastActivity")]
        public DateTime? LastActivity { get; set; }

        /// <summary>
        /// ID должности.
        /// </summary>
        [Column("PostId")]
        [Display(Name = "PostId")]
        public int? PostId { get; set; }

        /// <summary>
        /// Навигационное свойство для указания внешнего ключа PostId.
        /// </summary>
        [ForeignKey("PostId")]
        public Post? Post { get; set; }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public User()
        {

        }

        /// <summary>
        /// Конструктор пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="surname">Фамилия.</param>
        /// <param name="name">Имя.</param>
        /// <param name="patronymic">Отчество.</param>
        public User(string login, string surname, string name, string patronymic)
        {
            Login = login;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
        }
    }
}
