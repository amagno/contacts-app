using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.ViewModels
{

    public class ContactViewModel
    {
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^m$|^f$")]
        public string Gender { get; set; }
        [Required]
        public bool IsFavorite { get; set; }
        [Required]
        [MinLength(3)]
        public string Company { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Avatar { get; set; }
        [MinLength(3)]
        public string Address { get; set; }
        [MinLength(3)]
        public string Phone { get; set; }
        public string Comments { get; set; }
    }
}