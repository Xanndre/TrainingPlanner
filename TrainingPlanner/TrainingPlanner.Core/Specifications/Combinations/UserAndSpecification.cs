using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Combinations
{
    public class UserAndSpecification : ISpecification<ApplicationUser>
    {
        public ISpecification<ApplicationUser> Left { get; set; }
        public ISpecification<ApplicationUser> Right { get; set; }
        public UserAndSpecification(ISpecification<ApplicationUser> left, ISpecification<ApplicationUser> right)
        {
            Left = left;
            Right = right;
        }
        public bool IsSatisfiedBy(ApplicationUser user) => Left.IsSatisfiedBy(user) && Right.IsSatisfiedBy(user);
    }
}
