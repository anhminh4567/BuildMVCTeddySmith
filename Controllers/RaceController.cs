using BuildMVCTeddySmith.DatabaseStuff;
using BuildMVCTeddySmith.DatabaseStuff.Model;
using BuildMVCTeddySmith.Interface;
using BuildMVCTeddySmith.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildMVCTeddySmith.Controllers
{
    public class RaceController : Controller
    {
        private readonly RaceRepository raceRepository;
        public RaceController(RaceRepository repository)
        {
            this.raceRepository = repository;
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
		public async Task<IActionResult> Create(Race newRace)
		{
			if (ModelState.IsValid == false)
			{
				return View(newRace);
			}
			else
			{
				bool result = raceRepository.Add(newRace);
				return RedirectToAction("Index");
			}
		}
	}
}
