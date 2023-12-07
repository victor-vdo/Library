namespace Library
{
    public class User : Model
    {
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
    }
}
