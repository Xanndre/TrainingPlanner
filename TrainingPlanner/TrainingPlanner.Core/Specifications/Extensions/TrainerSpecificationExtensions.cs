using TrainingPlanner.Core.Specifications.Combinations;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Extensions
{
    public static class TrainerSpecificationExtensions
    {
        public static ISpecification<Trainer> And(this ISpecification<Trainer> left, ISpecification<Trainer> right)
        {
            return new TrainerAndSpecification(left, right);
        }
    }
}
