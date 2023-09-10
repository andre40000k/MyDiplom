using LoginComponent.Models.ParcelModel;

namespace LoginComponent.Models.Department
{
    public class LocalDepartment : IBaseDepartment
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public Guid DistrictId { get; set; }
        public DistrictDepartment DistrictDepartment { get; set; }
        public ICollection<Parcel> UserPackegs { get; set; }
    }
}
