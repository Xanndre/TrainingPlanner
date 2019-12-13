using System.Collections.Generic;
using System.Linq;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.TrainerFilters
{
    public class TrainerMatchesSports : ISpecification<Trainer>
    {
        public List<int?> SportIds { get; set; }
        public TrainerMatchesSports(List<int?> sportIds)
        {
            SportIds = sportIds;
        }

        public bool IsSatisfiedBy(Trainer trainer)
        {
            return SportIds == null || SportIds.All(id => trainer.Sports.Any(sport => sport.SportId == id));
        }
    }
}
