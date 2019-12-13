using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.ClubRateFilters
{
    public class ClubRateMatchesRate : ISpecification<ClubRate>
    {
        public int? Rate { get; set; }
        public ClubRateMatchesRate(int? rate)
        {
            Rate = rate;
        }

        public bool IsSatisfiedBy(ClubRate clubRate)
        {
            return Rate == null || clubRate.Rate == Rate;
        }
    }
}
