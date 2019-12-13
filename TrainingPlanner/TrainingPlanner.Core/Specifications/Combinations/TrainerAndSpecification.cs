using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Combinations
{
    public class TrainerAndSpecification : ISpecification<Trainer>
    {
        public ISpecification<Trainer> Left { get; set; }
        public ISpecification<Trainer> Right { get; set; }
        public TrainerAndSpecification(ISpecification<Trainer> left, ISpecification<Trainer> right)
        {
            Left = left;
            Right = right;
        }
        public bool IsSatisfiedBy(Trainer trainer) => Left.IsSatisfiedBy(trainer) && Right.IsSatisfiedBy(trainer);
    }
}
