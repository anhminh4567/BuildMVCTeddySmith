using BuildMVCTeddySmith.DatabaseStuff.Model;

namespace BuildMVCTeddySmith.Interface
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAllClubsAsync();
        Task<Club> GetClubByIdAsync(int id);
        Task<Club> GetClubByCity(string city);
        bool Add(Club club);
        bool Update(Club club); 
        bool Delete(Club club);
        bool Save();
    }
}
