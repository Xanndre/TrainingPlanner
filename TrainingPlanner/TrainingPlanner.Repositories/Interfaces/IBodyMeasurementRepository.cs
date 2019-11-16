using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Repositories.Interfaces
{
    public interface IBodyMeasurementRepository
    {
        Task<BodyMeasurement> UpdateBodyMeasurement(BodyMeasurement measurement);
        Task<BodyMeasurement> CreateBodyMeasurement(BodyMeasurement measurement);
        Task DeleteBodyMeasurement(BodyMeasurement measurement);
        Task<IEnumerable<BodyMeasurement>> GetBodyMeasurements(string userId);
        Task<BodyMeasurement> GetBodyMeasurement(int id);
        Task<IEnumerable<BodyInjury>> GetBodyInjuriesToDelete(BodyMeasurement measurement);
        Task RemoveBodyInjuries(IEnumerable<BodyInjury> injuries, bool isSavingChanges = true);
    }
}
