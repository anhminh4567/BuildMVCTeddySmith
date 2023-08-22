using BuildMVCTeddySmith.DatabaseStuff;
using BuildMVCTeddySmith.DatabaseStuff.Model;
using BuildMVCTeddySmith.Dto;
using BuildMVCTeddySmith.Interface;
using BuildMVCTeddySmith.Repo;
using BuildMVCTeddySmith.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildMVCTeddySmith.Controllers
{
    public class RaceController : Controller
    {
        private readonly RaceRepository raceRepository;
        private readonly IPhotoService _photoService;
        public RaceController(RaceRepository repository, IPhotoService photoService)
        {
            this.raceRepository = repository;
            this._photoService = photoService;   
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> getAllRace = await raceRepository.GetAllRacesAsync();
            return View(getAllRace);
        }
        public async Task<IActionResult> Details(int id)
        {
            Race getClub = await raceRepository.GetRaceByIdAsync(id);
            return View(getClub);
        }
        public IActionResult Create()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> Create(CreateRaceDto newRace)
		{
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(newRace.Image);
                var race = new Race
                {
                    Title = newRace.Title,
                    Description = newRace.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = newRace.Address.Street,
                        City = newRace.Address.City,
                        State = newRace.Address.State,
                    },  
                };
                bool addResult = raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "photo upload failed");
                return View(newRace);
            }
        }
	}
}
