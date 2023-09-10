namespace LoginComponent.Models.ParcelModel
{
    public class Location
    {
        public Guid Id { get; set; }
       
        public Guid DepartmentId { get; set; }
        public string DepartmentAddress { get; set; }

        public Guid ParcelId { get; set; }
        public Parcel Parcel { get; set; }
    }
}
