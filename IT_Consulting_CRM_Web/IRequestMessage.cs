public interface IRequestMessage
{
    int Id { get; set; }
    string Name { get; set; }
    string Email { get; set; }
    string Message { get; set; }
    DateTime Date { get; set; }
}

