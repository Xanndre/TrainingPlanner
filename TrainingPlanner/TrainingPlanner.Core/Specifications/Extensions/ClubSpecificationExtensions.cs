using TrainingPlanner.Core.Specifications.Combinations;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Extensions
{
    public static class ClubSpecificationExtensions
    {
        public static ISpecification<Club> And(this ISpecification<Club> left, ISpecification<Club> right)
        {
            return new ClubAndSpecification(left, right);
        }
    }
}
