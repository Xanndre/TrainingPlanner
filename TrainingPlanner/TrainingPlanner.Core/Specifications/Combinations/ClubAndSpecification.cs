using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Combinations
{
    public class ClubAndSpecification : ISpecification<Club>
    {
        public ISpecification<Club> Left { get; set; }
        public ISpecification<Club> Right { get; set; }
        public ClubAndSpecification(ISpecification<Club> left, ISpecification<Club> right)
        {
            Left = left;
            Right = right;
        }
        public bool IsSatisfiedBy(Club club) => Left.IsSatisfiedBy(club) && Right.IsSatisfiedBy(club);
    }
}
