namespace LoginComponent.Models.Transports
{
    public class Transport
    {
        public Transport()
        {
            UserPackegs = new HashSet<UserPackeg>();
            Departments = new HashSet<Department>();
        }

        public Guid Id { get; set; }
        public int Capacity { get; set; }
        public string TypeOfTransport { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<UserPackeg> UserPackegs { get; set; }


    }
}
