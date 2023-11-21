namespace SoftPortalBot.Client.Models
{
    /// <summary>
    /// ������ ������������� ������.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// ID �������.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// �������� �� ID �������.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}