using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.ClubCardFilters
{
    public class ClubCardMatchesName : ISpecification<ClubCard>
    {
        public string Name { get; set; }
        public ClubCardMatchesName(string name)
        {
            Name = name;
        }

        public bool IsSatisfiedBy(ClubCard clubCard)
        {
            return Name == null || clubCard.Name == Name;
        }
    }
}
