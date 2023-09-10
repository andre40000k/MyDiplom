using LoginComponent.Models.ParcelModel;

namespace LoginComponent.Models.Transports
{
    public class Transport
    {
        //public Transport()
        //{
        //    //UserPackegs = new HashSet<UserPackeg>();
        //    //Departments = new HashSet<BaseDepartment>();
        //}

        public Guid Id { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; }
        public string StartLocation { get; set; }
        public string CurrentLocation { get; set; }
        public string EndLocation { get; set; }
        public ICollection<Parcel> UserPackegs { get; set; }


        //public virtual ICollection<BaseDepartment> Departments { get; set; }
        //


    }
}
