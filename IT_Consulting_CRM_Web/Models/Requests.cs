﻿namespace IT_Consulting_CRM_Web.Models
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
        public Requests(int id, string name, string surName, string email, string information)
        {
            Id = id;
            Status = 0;
            Name = name;
            SurName = surName;
            EMail = email;
            Information = information;
            Date = DateTime.Now;
        }
    }
}
