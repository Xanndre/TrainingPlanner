using TrainingPlanner.Core.Specifications.Combinations;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Extensions
{
    public static class UserSpecificationExtensions
    {
        public static ISpecification<ApplicationUser> And(this ISpecification<ApplicationUser> left, ISpecification<ApplicationUser> right)
        {
            return new UserAndSpecification(left, right);
        }
    }
}
