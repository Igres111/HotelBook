using AdminPanel.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.HotelInterfaces;

namespace AdminPanel.Controllers
{
    public class HotelsController : Controller
    {
        public readonly IHotel _hotelService;
        public HotelsController(IHotel hotelService)
        {
            _hotelService = hotelService;
        }
        public async Task<IActionResult> Index()
        {
            var hotels = await _hotelService.GetAllHotels();
            var hotelViewModels = hotels.Select(h => new HotelViewModel
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                City = h.City,
                Country = h.Country,
            }).ToList();
            return View(hotelViewModels);
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _hotelService.DeleteHotel(id);
            TempData["Message"] = "Hotel deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
