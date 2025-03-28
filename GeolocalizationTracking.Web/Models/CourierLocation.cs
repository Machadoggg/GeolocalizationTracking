namespace GeolocalizationTracking.Web.Models
{
    public class CourierLocation
    {
        public string CourierId { get; set; } = default!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
