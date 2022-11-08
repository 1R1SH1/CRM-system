namespace IT_Consulting_CRM_API.Models
{
    public class AnswerUser
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public AnswerUser(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
