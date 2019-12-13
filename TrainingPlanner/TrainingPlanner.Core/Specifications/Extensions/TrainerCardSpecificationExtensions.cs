using TrainingPlanner.Core.Specifications.Combinations;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Extensions
{
    public static class TrainerCardSpecificationExtensions
    {
        public static ISpecification<TrainerCard> And(this ISpecification<TrainerCard> left, ISpecification<TrainerCard> right)
        {
            return new TrainerCardAndSpecification(left, right);
        }
    }
}
