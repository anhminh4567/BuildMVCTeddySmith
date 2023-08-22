using BuildMVCTeddySmith.DatabaseStuff;
using BuildMVCTeddySmith.DatabaseStuff.Model;
using BuildMVCTeddySmith.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Diagnostics;
namespace BuildMVCTeddySmith.Repo
{
    public class ClubRepository : IClubRepository
    {
        private readonly DatabaseContext databaseContext;
        public ClubRepository(DatabaseContext context)
        {
            this.databaseContext = context;
        }

        public async Task<IEnumerable<Club>> GetAllClubsAsync()
        {
            return await databaseContext.Clubs.ToListAsync();
        }

        public async Task<Club> GetClubByIdAsync(int id)
        {
            return await databaseContext.Clubs.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Club> GetClubByCity(string city)
        {
            return await databaseContext.Clubs.Where(c => c.Address.City.Contains(city)).FirstOrDefaultAsync();
        }
        public bool Add(Club club)
        {
            databaseContext.Clubs.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {
            databaseContext.Clubs.Remove(club);
            return Save();
        }
        public bool Update(Club club)
        {
            databaseContext.Clubs.Update(club);
            return Save();
        }
        public bool Save()
        {
            try {
                databaseContext.SaveChanges();
                return true;
            }catch(Exception ex) { 
                Debug.WriteLine(ex);
                return false;
            }
            
        }




    }
}
