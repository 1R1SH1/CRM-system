namespace Desktop_App.Models
{
    public class Services
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string ServicesInformation { get; set; }
        public Services(int id, string header, string description)
        {
            Id = id;
            Header = header;
            ServicesInformation = description;
        }
    }
}
