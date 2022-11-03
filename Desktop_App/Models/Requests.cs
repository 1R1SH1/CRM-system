using System;

namespace Desktop_App.Models
{
    public class Requests
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string EMail { get; set; }
        public string Information { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public Requests() { }
    }
}
