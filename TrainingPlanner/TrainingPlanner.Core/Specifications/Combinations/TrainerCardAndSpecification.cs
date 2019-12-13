using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Combinations
{
    public class TrainerCardAndSpecification : ISpecification<TrainerCard>
    {
        public ISpecification<TrainerCard> Left { get; set; }
        public ISpecification<TrainerCard> Right { get; set; }
        public TrainerCardAndSpecification(ISpecification<TrainerCard> left, ISpecification<TrainerCard> right)
        {
            Left = left;
            Right = right;
        }
        public bool IsSatisfiedBy(TrainerCard rate) => Left.IsSatisfiedBy(rate) && Right.IsSatisfiedBy(rate);
    }
}
