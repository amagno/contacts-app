using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsAPI.Models
{
    [Table("contacts_info")]
    public class ContactInfo
    {
        public int Id { get; set; }
        [Required]
        public string Company { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Comments { get; set; }
    }
}