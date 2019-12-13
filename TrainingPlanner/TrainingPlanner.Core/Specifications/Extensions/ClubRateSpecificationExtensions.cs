using TrainingPlanner.Core.Specifications.Combinations;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Extensions
{
    public static class ClubRateSpecificationExtensions
    {
        public static ISpecification<ClubRate> And(this ISpecification<ClubRate> left, ISpecification<ClubRate> right)
        {
            return new ClubRateAndSpecification(left, right);
        }
    }
}
