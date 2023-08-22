using BuildMVCTeddySmith.DatabaseStuff;
using BuildMVCTeddySmith.DatabaseStuff.Model;
using BuildMVCTeddySmith.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildMVCTeddySmith.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        public ClubController(IClubRepository clubRepository) {
            this._clubRepository = clubRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAllClubsAsync();
            return View(clubs);
        }
        public async Task<IActionResult> Details(int id)
        {
            Club getClub =await _clubRepository.GetClubByIdAsync(id);
            return View(getClub);
        }
        public IActionResult CreatePage() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePage(Club newClub) {
            if (ModelState.IsValid == false)
            {
                return View(newClub);
            }
            else {
                bool result =_clubRepository.Add(newClub);
                return RedirectToAction("Index");
			}
        }
    }
}
