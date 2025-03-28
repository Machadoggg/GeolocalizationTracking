using GeolocalizationTracking.Web.Models;

namespace GeolocalizationTracking.Mobile
{
    public class LocationService : ILocationService
    {
        private readonly ICourierService _courierService;
        private CancellationTokenSource _cts;
        private string _currentCourierId;

        public LocationService(ICourierService courierService)
        {
            _courierService = courierService;
        }

        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(
                    GeolocationAccuracy.Best,
                    TimeSpan.FromSeconds(10));

                var location = await Geolocation.Default.GetLocationAsync(request);
                return location;
            }
            catch (Exception ex)
            {
                // Handle exception
                return null;
            }
        }

        public async Task StartTracking(string courierId)
        {
            _currentCourierId = courierId;
            _cts = new CancellationTokenSource();

            await Task.Run(async () =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    var location = await GetCurrentLocation();
                    if (location != null)
                    {
                        await _courierService.UpdateCourierLocation(new CourierLocation
                        {
                            CourierId = courierId,
                            Latitude = location.Latitude,
                            Longitude = location.Longitude,
                            Timestamp = DateTime.UtcNow
                        });
                    }
                    await Task.Delay(5000); // Update every 5 seconds
                }
            }, _cts.Token);
        }

        public Task StopTracking()
        {
            _cts?.Cancel();
            return Task.CompletedTask;
        }
    }
}
