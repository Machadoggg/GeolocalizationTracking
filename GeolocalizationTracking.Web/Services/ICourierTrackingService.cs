using GeolocalizationTracking.Web.Models;
using System.Collections.Concurrent;

namespace GeolocalizationTracking.Web.Services
{
    public interface ICourierTrackingService
    {
        void UpdateCourierLocation(CourierLocation location);
        IEnumerable<CourierLocation> GetActiveCouriers();
        CourierLocation GetCourierLocation(string courierId);
    }

    public class CourierTrackingService : ICourierTrackingService
    {
        private readonly ConcurrentDictionary<string, CourierLocation> _courierLocations = new();

        public void UpdateCourierLocation(CourierLocation location)
        {
            _courierLocations.AddOrUpdate(location.CourierId, location, (id, old) => location);
        }

        public IEnumerable<CourierLocation> GetActiveCouriers()
        {
            return _courierLocations.Values;
        }

        public CourierLocation GetCourierLocation(string courierId)
        {
            _courierLocations.TryGetValue(courierId, out var location);
            return location;
        }
    }
}
