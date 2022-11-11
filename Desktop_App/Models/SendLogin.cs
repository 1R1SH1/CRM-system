namespace Desktop_App.Models
{
    public class SendLogin
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public SendLogin()
        {

        }

        public SendLogin(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
