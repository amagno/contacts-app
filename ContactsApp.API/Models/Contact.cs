using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsAPI.Models
{
    [Table("contacts")]
    public class Contact
    {
        public Contact() 
        {
            Created = DateTime.Now;
        }
        public Contact(
            string firstName, 
            string lastName, 
            string email, 
            string gender, 
            bool isFavorite,
            string company,
            string avatar = null,
            string address = null,
            string phone = null,
            string comments = null

        )
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Gender = gender;
            IsFavorite = isFavorite;
            Created = DateTime.Now;
            Info = new ContactInfo
            {
                Avatar = avatar ?? DefaultAvatar,
                Company = company,
                Address = address,
                Phone = phone,
                Comments = comments
            };
        }
        [NotMapped]
        public readonly string DefaultAvatar = "https://png.pngtree.com/element_pic/00/16/07/2857995a340e5e6.jpg";
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^m$|^f$")]
        public string Gender { get; set; }
        [Required]
        public bool IsFavorite { get; set; }
        public ContactInfo Info { get; set; }
        [Required]
        public DateTime Created { get; set; }

    }
}