namespace Desktop_App.Models
{
    public class SendLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public SendLogin()
        {

        }

        public SendLogin(string name, string password)
        {
            Username = name;
            Password = password;
        }
    }
}
