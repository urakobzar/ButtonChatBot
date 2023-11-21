using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SoftPortalBot.Model.DataBaseTable
{
    /// <summary>
    /// Должность.
    /// </summary>
    [Table("dictPosts")]
    public class Post
    {
        /// <summary>
        /// ID.
        /// </summary>
        [Column("Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// Утверждена ли должность.
        /// </summary>
        [Column("IsApproversApprovalAvailable")]
        [Display(Name = "IsApproversApprovalAvailable")]
        public bool IsApproversApprovalAvailable { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        [Column("Name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public Post()
        {

        }
    }
}
