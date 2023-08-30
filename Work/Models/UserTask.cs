namespace LoginComponent.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime Ts { get; set; }
        public virtual User User { get; set; }
    }
}
