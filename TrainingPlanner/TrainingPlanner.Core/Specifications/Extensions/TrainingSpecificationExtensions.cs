using TrainingPlanner.Core.Specifications.Combinations;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Extensions
{
    public static class TrainingSpecificationExtensions
    {
        public static ISpecification<Training> And(this ISpecification<Training> left, ISpecification<Training> right)
        {
            return new TrainingAndSpecification(left, right);
        }
    }
}
