using System.Collections.ObjectModel;

namespace GeolocalizationTracking.Mobile
{
    public class CourierService : ICourierService
    {
        private HubConnection _hubConnection;
        public ObservableCollection<CourierLocation> ActiveCouriers { get; } = new();

        public async Task Connect()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://yourapi.com/locationHub")
                .Build();

            _hubConnection.On<CourierLocation>("LocationUpdated", location =>
            {
                var existing = ActiveCouriers.FirstOrDefault(c => c.CourierId == location.CourierId);
                if (existing != null)
                {
                    var index = ActiveCouriers.IndexOf(existing);
                    ActiveCouriers[index] = location;
                }
                else
                {
                    ActiveCouriers.Add(location);
                }
            });

            await _hubConnection.StartAsync();

            // Get initial couriers
            var couriers = await _hubConnection.InvokeAsync<IEnumerable<CourierLocation>>("GetActiveCouriers");
            foreach (var courier in couriers)
            {
                ActiveCouriers.Add(courier);
            }
        }

        public async Task UpdateCourierLocation(CourierLocation location)
        {
            if (_hubConnection?.State == HubConnectionState.Connected)
            {
                await _hubConnection.InvokeAsync("UpdateLocation", location);
            }
        }

        public async Task<ObservableCollection<CourierLocation>> GetActiveCouriers()
        {
            return ActiveCouriers;
        }
    }
}
