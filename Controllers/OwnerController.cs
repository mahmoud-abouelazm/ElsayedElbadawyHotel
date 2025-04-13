using ElSayedHotel.IRepository;
using ElSayedHotel.Repository;
using ElSayedHotel.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElSayedHotel.Controllers
{
    public class OwnerController : Controller
    {
        private readonly IRoomRepository roomRepository;

        public OwnerController(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }
        [Authorize]
        public async Task<IActionResult> ShowProperties()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var propertiesList = await roomRepository.GetOwnerPropertiesAsync(userId);
            return View(propertiesList);
        }
    }
}
