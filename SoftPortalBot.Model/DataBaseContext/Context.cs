using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using SoftPortalBot.Model.DataBaseTable;

namespace SoftPortalBot.Model.DataBaseContext
{
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    public class Context : DbContext
    {
        /// <summary>
        /// Конструктор контекста базы данных.
        /// </summary>
        /// <param name="options">Настройки базы данных.</param>
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Конструктор контекста базы данных.
        /// </summary>
        public Context() => Database.EnsureCreated();

        /// <summary>
        /// Конфигурация базы данных.
        /// </summary>
        /// <param name="optionsBuilder">Настройки базы данных.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                        "Host = localhost; Port = 5432; Username = postgres;" +
                        "Password = 1234; Database = MyChatBot;");
        }

        /// <summary>
        /// Установка свойств в базе данных.
        /// </summary>
        /// <param name="modelBuilder">Модель создания сущности.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserResponsibleGroup>()
                .HasIndex(p => new { p.UserId, p.ResponsibleGroupId }).IsUnique();
            modelBuilder.Entity<ApplicationResponsibleGroup>()
                .HasIndex(p => new { p.ApplicationId, p.ResponsibleGroupId }).IsUnique();
        }

        /// <summary>
        /// Таблица "Приложения".
        /// </summary>
        public DbSet<Application> Applications { get; set; } = null!;

        /// <summary>
        /// Таблица связи "Приложение Ответственная группа".
        /// </summary>
        public DbSet<ApplicationResponsibleGroup> ApplicationResponsibleGroups { get; set; } = null!;

        /// <summary>
        /// Таблица "Тип приложения".
        /// </summary>
        public DbSet<AppType> AppTypes { get; set; } = null!;

        /// <summary>
        /// Таблица Функции группы.
        /// </summary>
        public DbSet<GroupFunction> GroupFunctions { get; set; } = null!;

        /// <summary>
        /// Таблица "Вопрос базы знаний".
        /// </summary>
        public DbSet<ProblemAnswer> ProblemAnswers { get; set; } = null!;

        /// <summary>
        /// Таблица "Ответ базы знаний".
        /// </summary>
        public DbSet<ProblemResponse> ProblemResponses { get; set; } = null!;

        /// <summary>
        /// Таблица "Должности".
        /// </summary>
        public DbSet<Post> Posts { get; set; } = null!;

        /// <summary>
        /// Таблица "Заявки".
        /// </summary>
        public DbSet<Request> Requests { get; set; } = null!;

        /// <summary>
        /// Таблица "Статус заявки".
        /// </summary>
        public DbSet<RequestStatus> RequestStatus { get; set; } = null!;

        /// <summary>
        /// Таблица "Причина обращения".
        /// </summary>
        public DbSet<RequestReason> RequestReasons { get; set; } = null!;

        /// <summary>
        /// Таблица "Ответственная группа".
        /// </summary>
        public DbSet<ResponsibleGroup> ResponsibleGroups { get; set; } = null!;

        /// <summary>
        /// Таблица связи "Функции Ответственная группа".
        /// </summary>
        public DbSet<ResponsibleGroupFunction> ResponsibleGroupFunctions { get; set; } = null!;

        /// <summary>
        /// Таблица "Роли".
        /// </summary>
        public DbSet<Role> Roles { get; set; } = null!;

        /// <summary>
        /// Таблица связи "Пользователь Роль".
        /// </summary>
        public DbSet<RoleUser> RoleUsers { get; set; } = null!;

        /// <summary>
        /// Таблица "Изменение статуса".
        /// </summary>
        public DbSet<StatusChange> StatusChanges { get; set; }

        /// <summary>
        /// Таблица "Пользователи".
        /// </summary>
        public DbSet<User> Users { get; set; }
        
        /// <summary>
        /// Таблица связи "Пользователь Ответственная группа".
        /// </summary>
        public DbSet<UserResponsibleGroup> UserResponsibleGroup { get; set; }
    }
}
