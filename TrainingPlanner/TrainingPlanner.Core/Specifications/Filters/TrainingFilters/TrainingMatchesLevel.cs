using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.TrainingFilters
{
    public class TrainingMatchesLevel : ISpecification<Training>
    {
        public string Level { get; set; }
        public TrainingMatchesLevel(string level)
        {
            Level = level;
        }

        public bool IsSatisfiedBy(Training training)
        {
            return Level == null || training.Level == Level;
        }
    }
}
