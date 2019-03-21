using System;

namespace ContactsApp.Domain
{
    public class Contact
    {
        private bool _isFavorite;
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Birthday { get; private set; }
        public string Email { get; private set; }
        public string Gender { get; private set; }
        public bool IsFavorite { get => _isFavorite; }
        public ContactInfo Info { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Updated { get; private set; }

        // APENAS PARA ENTITY-FRAMEWORK
        protected Contact() {}
        public Contact(
            string firstName, 
            string lastName, 
            string email,  
            DateTime birthDay,
            string gender, 
            bool isFavorite,
            ContactInfo info
        )
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Gender = gender;
            Created = DateTime.Now;
            Birthday = birthDay;
            SetIsFavorite(isFavorite);
            Info = info;
            Updated = null;
        }
        public void EditContact(
            string firstName, 
            string lastName, 
            string email, 
            DateTime birthDay,
            string gender, 
            bool isFavorite,
            ContactInfo info
        ) 
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Gender = gender;
            Birthday = birthDay;
            SetIsFavorite(isFavorite);
            Updated = DateTime.Now;
            Info = info;
        }
        public void SetIsFavorite(bool isFavorite)
        {
            if (isFavorite == true)
            {
                var age = (DateTime.Now.Year - Birthday.Year);
                if (age < 18) throw new InvalidFavoriteContactException(age);
            }

            _isFavorite = isFavorite;
            Updated = DateTime.Now;
        }

    }
}