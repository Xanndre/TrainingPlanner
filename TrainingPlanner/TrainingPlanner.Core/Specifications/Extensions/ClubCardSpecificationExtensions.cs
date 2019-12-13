using TrainingPlanner.Core.Specifications.Combinations;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Extensions
{
    public static class ClubCardSpecificationExtensions
    {
        public static ISpecification<ClubCard> And(this ISpecification<ClubCard> left, ISpecification<ClubCard> right)
        {
            return new ClubCardAndSpecification(left, right);
        }
    }
}
