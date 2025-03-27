using GeolocalizationTracking.Web.Services;
using Microsoft.AspNetCore.SignalR;

namespace GeolocalizationTracking.Web.Hubs
{
    public class CourierHub : Hub
    {
        private readonly ICourierTrackingService _trackingService;

        public CourierHub(ICourierTrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        public override async Task OnConnectedAsync()
        {
            // Enviar todos los mensajeros activos al nuevo cliente
            var activeCouriers = _trackingService.GetActiveCouriers();
            await Clients.Caller.SendAsync("ReceiveActiveCouriers", activeCouriers);

            await base.OnConnectedAsync();
        }

        public async Task RequestCourierLocations()
        {
            var activeCouriers = _trackingService.GetActiveCouriers();
            await Clients.Caller.SendAsync("ReceiveActiveCouriers", activeCouriers);
        }
    }

}
