using System.ComponentModel.DataAnnotations;

namespace LoginComponent.Models.Request.Admin
{
    public class TransportRequest
    {
        [Required]
        public int Capacity { get; set; }
        [Required]
        public string TypeOfTransport { get; set; }
    }
}
