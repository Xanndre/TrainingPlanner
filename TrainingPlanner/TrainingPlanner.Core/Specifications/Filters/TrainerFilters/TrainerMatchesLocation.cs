using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.TrainerFilters
{
    public class TrainerMatchesLocation : ISpecification<Trainer>
    {
        public string Location { get; set; }
        public TrainerMatchesLocation(string location)
        {
            Location = location;
        }

        public bool IsSatisfiedBy(Trainer trainer)
        {
            return Location == null || trainer.User.City == Location;
        }
    }
}
