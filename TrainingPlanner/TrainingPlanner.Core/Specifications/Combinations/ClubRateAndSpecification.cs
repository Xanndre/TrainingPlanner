using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Combinations
{
    public class ClubRateAndSpecification : ISpecification<ClubRate>
    {
        public ISpecification<ClubRate> Left { get; set; }
        public ISpecification<ClubRate> Right { get; set; }
        public ClubRateAndSpecification(ISpecification<ClubRate> left, ISpecification<ClubRate> right)
        {
            Left = left;
            Right = right;
        }
        public bool IsSatisfiedBy(ClubRate rate) => Left.IsSatisfiedBy(rate) && Right.IsSatisfiedBy(rate);
    }
}
