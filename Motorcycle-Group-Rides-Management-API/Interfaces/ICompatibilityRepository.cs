using Motorcycle_Group_Rides_Management_API.Helpers;
using Motorcycle_Group_Rides_Management_API.Models;

namespace Motorcycle_Group_Rides_Management_API.Interfaces
{
    public interface ICompatibilityRepository
    {
       
        Compatibility GetCompatibilityById(int compatibilityId);
        void AddCompatibility(Compatibility compatibility);
        void UpdateCompatibility(Compatibility compatibility);
        void DeleteCompatibility(Compatibility compatibility);
        void SaveChanges();

    }
}
