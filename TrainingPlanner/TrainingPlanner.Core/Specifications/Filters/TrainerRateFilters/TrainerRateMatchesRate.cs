using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.TrainerRateFilters
{
    public class TrainerRateMatchesRate : ISpecification<TrainerRate>
    {
        public int? Rate { get; set; }
        public TrainerRateMatchesRate(int? rate)
        {
            Rate = rate;
        }

        public bool IsSatisfiedBy(TrainerRate trainerRate)
        {
            return Rate == null || trainerRate.Rate == Rate;
        }
    }
}
