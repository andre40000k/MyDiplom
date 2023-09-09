namespace LoginComponent.Models.Department
{
    public class RegionalDepartment : IBaseDepartment
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public ICollection<DistrictDepartment> DistrictDepartments { get; set; }
    }
}
