using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Combinations
{
    public class TrainingAndSpecification : ISpecification<Training>
    {
        public ISpecification<Training> Left { get; set; }
        public ISpecification<Training> Right { get; set; }
        public TrainingAndSpecification(ISpecification<Training> left, ISpecification<Training> right)
        {
            Left = left;
            Right = right;
        }
        public bool IsSatisfiedBy(Training training) => Left.IsSatisfiedBy(training) && Right.IsSatisfiedBy(training);
    }
}
