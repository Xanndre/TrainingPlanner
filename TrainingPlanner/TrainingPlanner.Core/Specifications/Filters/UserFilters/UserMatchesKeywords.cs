using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.UserFilters
{
    public class UserMatchesKeywords : ISpecification<ApplicationUser>
    {
        public string Keywords { get; set; }
        public UserMatchesKeywords(string keywords)
        {
            Keywords = keywords;
        }

        public bool IsSatisfiedBy(ApplicationUser user)
        {
            return Keywords == null ||
                user.FirstName.ToLower().Contains(Keywords.ToLower()) ||
                user.LastName.ToLower().Contains(Keywords.ToLower());
        }
    }
}
