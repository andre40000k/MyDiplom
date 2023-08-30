namespace LoginComponent.Models
{
    public class User
    {
        public User() 
        { 
            RefreshTokens = new HashSet<RefreshToken>();
            Tasks = new HashSet<UserTask>();
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Ts { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<UserTask> Tasks { get; set; }    

    }
}
