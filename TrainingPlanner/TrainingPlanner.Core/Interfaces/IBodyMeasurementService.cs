using System.Threading.Tasks;
using TrainingPlanner.Core.DTOs.BodyMeasurement;
using TrainingPlanner.Core.DTOs.Paged;

namespace TrainingPlanner.Core.Interfaces
{
    public interface IBodyMeasurementService
    {
        Task<BodyMeasurementDTO> UpdateBodyMeasurement(BodyMeasurementDTO measurement);
        Task<BodyMeasurementCreateDTO> CreateBodyMeasurement(BodyMeasurementCreateDTO measurement);
        Task DeleteBodyMeasurement(int id);
        Task<BodyMeasurementDTO> GetBodyMeasurement(int id);
        Task<PagedBodyMeasurementsDTO> GetAllBodyMeasurements(int pageNumber, int pageSize, string userId);
    }
}
