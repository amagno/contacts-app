using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactsApp.API.ViewModels
{

    public class EditContactViewModel
    {
        [MinLength(3)]
        public string FirstName { get; set; }
        [MinLength(3)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        [RegularExpression(@"^m$|^f$")]
        public string Gender { get; set; }
        public bool? IsFavorite { get; set; }
        [MinLength(3)]
        public string Company { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Avatar { get; set; }
        [MinLength(3)]
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string Comments { get; set; }
    }
}