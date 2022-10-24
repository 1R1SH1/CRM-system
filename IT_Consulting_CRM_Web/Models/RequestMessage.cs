using System;

namespace IT_Consulting_CRM_Web
{
    public class RequestMessage : IRequestMessage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public int Status { get; set; }
        public RequestMessage() { }
        public RequestMessage(int id, string name, string email, string message)
        {
            Id = id;
            Status = 1;
            Name = name;
            Email = email;
            Message = message;
            Date = DateTime.Now;
        }
    }
}
