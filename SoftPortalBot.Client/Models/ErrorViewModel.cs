namespace SoftPortalBot.Client.Models
{
    /// <summary>
    /// Ошибка представления модели.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// ID запроса.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Показано ли ID запроса.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}