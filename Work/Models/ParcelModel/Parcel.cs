using LoginComponent.Models.Transports;

namespace LoginComponent.Models.ParcelModel
{
    public class Parcel
    {
        public Guid Id { get; set; }

        public string StartLocation { get; set; }
        public string FinalLocation { get; set; }
        public ICollection<Location> CurrentLocation { get; set; }

        public double Weight { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
