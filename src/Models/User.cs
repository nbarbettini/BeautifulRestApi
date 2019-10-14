namespace BeautifulRestApi.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Guid { get; set; }
        public string Email { get; set; }


        public User(string guid, string name, string email)
        {
            Name = name;
            Guid = guid;
            Email = email;
        }
    }
}