using LoginComponent.Models.Transports;

namespace LoginComponent.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public Guid TransportId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Transport Transport { get; set; }
    }
}
