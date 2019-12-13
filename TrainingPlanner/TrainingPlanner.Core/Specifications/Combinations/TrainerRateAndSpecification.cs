using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Combinations
{
    public class TrainerRateAndSpecification : ISpecification<TrainerRate>
    {
        public ISpecification<TrainerRate> Left { get; set; }
        public ISpecification<TrainerRate> Right { get; set; }
        public TrainerRateAndSpecification(ISpecification<TrainerRate> left, ISpecification<TrainerRate> right)
        {
            Left = left;
            Right = right;
        }
        public bool IsSatisfiedBy(TrainerRate rate) => Left.IsSatisfiedBy(rate) && Right.IsSatisfiedBy(rate);
    }
}
