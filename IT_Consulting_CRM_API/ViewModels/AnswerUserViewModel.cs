namespace IT_Consulting_CRM_API.ViewModels
{
    public class AnswerUserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public AnswerUserViewModel(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
