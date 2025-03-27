using GeolocalizationTracking.Web.Models;
using GeolocalizationTracking.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GeolocalizationTracking.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourierTrackingService _trackingService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICourierTrackingService trackingService)
        {
            _logger = logger;
            _trackingService = trackingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCouriers()
        {
            var couriers = _trackingService.GetActiveCouriers();
            return Ok(couriers);
        }

        public IActionResult GetCourier(string id)
        {
            var courier = _trackingService.GetCourierLocation(id);
            return Ok(courier);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
