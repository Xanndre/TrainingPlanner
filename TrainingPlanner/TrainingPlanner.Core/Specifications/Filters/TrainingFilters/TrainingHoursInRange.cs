using System;
using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.TrainingFilters
{
    public class TrainingHoursInRange : ISpecification<Training>
    {
        public DateTime? LowerBound { get; set; }
        public DateTime? UpperBound { get; set; }
        public TrainingHoursInRange(DateTime? lowerBound, DateTime? upperBound)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        public bool IsSatisfiedBy(Training training)
        {
            return (LowerBound == null ||
                training.StartDate.TimeOfDay >= ((DateTime)LowerBound).TimeOfDay) && (UpperBound == null ||
                training.EndDate.TimeOfDay <= ((DateTime)UpperBound).TimeOfDay);
        }
    }
}
