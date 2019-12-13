using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.ClubFilters
{
    public class ClubMatchesKeywords : ISpecification<Club>
    {
        public string Keywords { get; set; }
        public ClubMatchesKeywords(string keywords)
        {
            Keywords = keywords;
        }

        public bool IsSatisfiedBy(Club club)
        {
            return Keywords == null ||
                club.Description.ToLower().Contains(Keywords.ToLower()) ||
                club.Name.ToLower().Contains(Keywords.ToLower());
        }
    }
}
