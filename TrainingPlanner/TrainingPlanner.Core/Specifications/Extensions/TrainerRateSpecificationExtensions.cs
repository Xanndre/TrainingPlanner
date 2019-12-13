using TrainingPlanner.Core.Specifications.Combinations;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Extensions
{
    public static class TrainerRateSpecificationExtensions
    {
        public static ISpecification<TrainerRate> And(this ISpecification<TrainerRate> left, ISpecification<TrainerRate> right)
        {
            return new TrainerRateAndSpecification(left, right);
        }
    }
}
