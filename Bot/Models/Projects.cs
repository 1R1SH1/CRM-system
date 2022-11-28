namespace Bot.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Image { get; set; }
        public string ProjectInformation { get; set; }
        public Projects() { }
        public Projects(int id, string header, string image, string description)
        {
            Id = id;
            Image = image;
            Header = header;
            ProjectInformation = description;
        }
    }
}
