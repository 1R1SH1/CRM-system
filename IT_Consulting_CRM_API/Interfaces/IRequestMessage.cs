
using System;

public interface IRequestMessage
{
    int Id { get; set; }
    string Name { get; set; }
    string Email { get; set; }
    string Message { get; set; }
    int Status { get; set; }
    /// <summary>
    /// Время отправления заявки.
    /// </summary>
    DateTime Date { get; set; }
}

