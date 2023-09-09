using LoginComponent.Models.Transports;

namespace LoginComponent.Models
{
    public class UserPackeg
    {
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public string TransportId { get; set; }
        public string CurrentLocation { get; set; }
        public string StartLocation { get; set; }
        public string FinalLocation { get; set; }
        public double Weight { get; set; }
        public User User { get; set; }
        public Transport Transport { get; set; }
    }
}
