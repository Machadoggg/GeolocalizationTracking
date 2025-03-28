namespace GeolocalizationTracking.Mobile
{
    public interface ILocationService
    {
        Task<Location> GetCurrentLocation();
        Task StartTracking(string courierId);
        Task StopTracking();
    }
}
