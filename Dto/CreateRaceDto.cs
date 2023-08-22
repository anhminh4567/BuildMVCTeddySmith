using BuildMVCTeddySmith.DatabaseStuff.Enum;
using BuildMVCTeddySmith.DatabaseStuff.Model;

namespace BuildMVCTeddySmith.Dto
{
    public class CreateRaceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public RaceCategory RaceCategory { get; set; }
    }
}
