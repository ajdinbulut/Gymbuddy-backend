namespace Gymbuddy.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }  
        public int Age { get; set; }
        public List<UserRole>? UserRoles { get; set; }

    }
}
