using GeolocalizationTracking.Web.Models;
using System.Collections.ObjectModel;

namespace GeolocalizationTracking.Mobile
{
    public interface ICourierService
    {
        Task UpdateCourierLocation(CourierLocation location);
        Task<ObservableCollection<CourierLocation>> GetActiveCouriers();
        Task Connect();
    }
}
