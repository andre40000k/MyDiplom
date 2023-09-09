using LoginComponent.Models.Transports;

namespace LoginComponent.Models
{
    public interface IBaseDepartment
    {
        public Guid Id { get; set; }        
        public int Number { get; set; }
        public string Address { get; set; }

        //public string? TransportId { get; set; }
        //public string DepartmentImportance { get; set; }
        //public ICollection<Transport> Transports { get; set; }
        //public ICollection<UserPackeg> userPackegs { get; set; }
        //public Transport Transport { get; set; }
    }
}
