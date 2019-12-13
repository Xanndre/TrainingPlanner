using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.ClubCardFilters
{
    public class ClubCardMatchesKeywords : ISpecification<ClubCard>
    {
        public string Keywords { get; set; }
        public ClubCardMatchesKeywords(string keywords)
        {
            Keywords = keywords;
        }

        public bool IsSatisfiedBy(ClubCard clubCard)
        {
            return Keywords == null ||
                clubCard.UserName.ToLower().Contains(Keywords.ToLower()) ||
                clubCard.ClubName.ToLower().Contains(Keywords.ToLower());
        }
    }
}
