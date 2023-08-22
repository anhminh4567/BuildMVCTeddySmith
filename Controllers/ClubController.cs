using BuildMVCTeddySmith.DatabaseStuff;
using BuildMVCTeddySmith.DatabaseStuff.Model;
using BuildMVCTeddySmith.Dto;
using BuildMVCTeddySmith.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildMVCTeddySmith.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        public ClubController(IClubRepository clubRepository,IPhotoService photoService) {
            this._clubRepository = clubRepository;
            this._photoService = photoService;
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
        public async Task<IActionResult> CreatePage(CreateClubDto newClub) 
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(newClub.Image);
                var club = new Club
                {
                    Title = newClub.Title,
                    Description = newClub.Description,
                    Image = result.Url.ToString(),
                    Address = new Address { 
                        Street = newClub.Address.Street,
                        City = newClub.Address.City,
                        State = newClub.Address.State,
                    },
                };
                bool addResult = _clubRepository.Add(club);
                return RedirectToAction("Index");
                
            }
            else {
                ModelState.AddModelError("","photo upload failed");
                return View(newClub);
            }
        }
    }
}
