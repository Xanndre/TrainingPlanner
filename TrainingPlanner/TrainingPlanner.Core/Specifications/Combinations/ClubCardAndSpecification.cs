using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Combinations
{
    public class ClubCardAndSpecification : ISpecification<ClubCard>
    {
        public ISpecification<ClubCard> Left { get; set; }
        public ISpecification<ClubCard> Right { get; set; }
        public ClubCardAndSpecification(ISpecification<ClubCard> left, ISpecification<ClubCard> right)
        {
            Left = left;
            Right = right;
        }
        public bool IsSatisfiedBy(ClubCard rate) => Left.IsSatisfiedBy(rate) && Right.IsSatisfiedBy(rate);
    }
}
