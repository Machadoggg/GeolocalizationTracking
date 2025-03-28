using GeolocalizationTracking.Web.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace GeolocalizationTracking.Web.Services
{
    public class CourierService : ICourierTrackingService
    {
        private HubConnection _hubConnection;

        public async Task Connect()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://tudominio.com/courierHub")
                .Build();

            await _hubConnection.StartAsync();
        }

        public IEnumerable<CourierLocation> GetActiveCouriers()
        {
            throw new NotImplementedException();
        }

        public CourierLocation GetCourierLocation(string courierId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCourierLocation(CourierLocation location)
        {
            if (_hubConnection?.State == HubConnectionState.Connected)
            {
                await _hubConnection.InvokeAsync("UpdateLocation", location);
            }
        }

        void ICourierTrackingService.UpdateCourierLocation(CourierLocation location)
        {
            throw new NotImplementedException();
        }
    }
}
