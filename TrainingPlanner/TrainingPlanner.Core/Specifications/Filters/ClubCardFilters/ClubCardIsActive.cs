using System;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.ClubCardFilters
{
    public class ClubCardIsActive : ISpecification<ClubCard>
    {
        public bool? IsActive { get; set; }
        public ClubCardIsActive(bool? isActive)
        {
            IsActive = isActive;
        }

        public bool IsSatisfiedBy(ClubCard clubCard)
        {
            return IsActive == null ||
                (clubCard.ExpirationDate > DateTime.Now) == IsActive;
        }
    }
}
