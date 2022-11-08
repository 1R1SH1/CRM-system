namespace IT_Consulting_CRM_Web.Models
{
    public class AnswerUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public AnswerUser(string id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }
    }
}
