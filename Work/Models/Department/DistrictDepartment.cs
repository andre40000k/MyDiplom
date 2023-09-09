namespace LoginComponent.Models.Department
{
    public class DistrictDepartment : IBaseDepartment
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public Guid RegionalID { get; set; }
        public RegionalDepartment RegionalDepartment { get; set; }
        public ICollection<LocalDepartment> LocalDepatments { get; set; }
    }
}
