namespace Desktop_App.Models
{
    public class Contacts
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public string ContactsInformation { get; set; }
        public Contacts(int id,
                        string image,
                        string name,
                        string surName,
                        string lastName,
                        string phone,
                        string eMail,
                        string address,
                        string inf)
        {
            Id = id;
            Image = image;
            Name = name;
            SurName = surName;
            LastName = lastName;
            Phone = phone;
            EMail = eMail;
            Address = address;
            ContactsInformation = inf;
        }
    }
}
