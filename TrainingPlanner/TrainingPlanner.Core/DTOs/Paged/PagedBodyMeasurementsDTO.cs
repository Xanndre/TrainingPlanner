using System.Collections.Generic;
using TrainingPlanner.Core.DTOs.BodyMeasurement;

namespace TrainingPlanner.Core.DTOs.Paged
{
    public class PagedBodyMeasurementsDTO : PagedDTO
    {
        public IEnumerable<BodyMeasurementBaseDTO> BodyMeasurements { get; set; }
    }
}
