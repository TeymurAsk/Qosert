using System.ComponentModel.DataAnnotations;

namespace ENS_API.Data
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public List<Contact> Contacts { get; set; } = new List<Contact>();
    }
}