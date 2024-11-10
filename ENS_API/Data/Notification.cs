using System.ComponentModel.DataAnnotations;

namespace ENS_API.Data
{
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public bool Status { get; set; } = false;
    }
}
