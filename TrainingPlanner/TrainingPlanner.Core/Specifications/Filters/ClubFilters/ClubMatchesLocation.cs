using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.ClubFilters
{
    public class ClubMatchesLocation : ISpecification<Club>
    {
        public string Location { get; set; }
        public ClubMatchesLocation(string location)
        {
            Location = location;
        }

        public bool IsSatisfiedBy(Club club)
        {
            return Location == null || club.City == Location;
        }
    }
}
