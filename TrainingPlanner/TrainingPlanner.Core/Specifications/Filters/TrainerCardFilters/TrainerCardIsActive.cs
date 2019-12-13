using System;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.TrainerCardFilters
{
    public class TrainerCardIsActive : ISpecification<TrainerCard>
    {
        public bool? IsActive { get; set; }
        public TrainerCardIsActive(bool? isActive)
        {
            IsActive = isActive;
        }

        public bool IsSatisfiedBy(TrainerCard trainerCard)
        {
            return IsActive == null ||
                (trainerCard.ExpirationDate > DateTime.Now) == IsActive;
        }
    }
}
