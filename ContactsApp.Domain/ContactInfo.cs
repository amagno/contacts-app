namespace ContactsApp.Domain
{
    public class ContactInfo
    {
        public int Id { get; private set; }
        public string Company { get; private set; }
        public string Avatar { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public string Comments { get; private set; }
        protected ContactInfo() {}
        public ContactInfo(
            string company = null,
            string avatar = null,
            string address = null,
            string phone = null,
            string comments = null
        )
        {
            Company = company;
            Avatar = avatar;
            Address = address;
            Phone = phone;
            Comments = comments;
        }
    }
}