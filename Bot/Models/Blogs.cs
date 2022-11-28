namespace Bot.Models
{
    public class Blogs
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Image { get; set; }
        public string BlogInformation { get; set; }
        public DateTime DateTime { get; set; }
        public Blogs(int id, string header, string image, string description, DateTime dateTime)
        {
            Id = id;
            Header = header;
            Image = image;
            BlogInformation = description;
            DateTime = dateTime;
        }
    }
}
