using BuildMVCTeddySmith.DatabaseStuff.Model;
using BuildMVCTeddySmith.DatabaseStuff;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace BuildMVCTeddySmith.Repo
{
    public class RaceRepository
    {
        private readonly DatabaseContext databaseContext;
        public RaceRepository(DatabaseContext context)
        {
            this.databaseContext = context;
        }

        public async Task<IEnumerable<Race>> GetAllRacesAsync()
        {
            return await databaseContext.Races.ToListAsync();
        }

        public async Task<Race> GetRaceByIdAsync(int id)
        {
            return await databaseContext.Races.Include(r => r.Address).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Race>> GetRacesByCity(string city)
        {
            return await databaseContext.Races.Where(r => r.Address.City.Contains(city)).ToListAsync();
        }
        public bool Add(Race race)
        {
            databaseContext.Races.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            databaseContext.Races.Remove(race);
            return Save();
        }
        public bool Update(Race race)
        {
            databaseContext.Races.Update(race);
            return Save();
        }
        public bool Save()
        {
            int result = databaseContext.SaveChanges();
            return result > 0 ? true : false;

        }
    }
}
