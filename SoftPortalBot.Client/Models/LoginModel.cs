using System.ComponentModel.DataAnnotations;

namespace SoftPortalBot.Client.Models
{
    /// <summary>
    /// Модель регистрации.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Логин.
        /// </summary>
        [Required(ErrorMessage = "Не указан Login")]
        public string? Login { get; set; }
        
    }
}
