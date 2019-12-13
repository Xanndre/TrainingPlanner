using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.TrainingFilters
{
    public class TrainingMatchesTitle : ISpecification<Training>
    {
        public string Title { get; set; }
        public TrainingMatchesTitle(string title)
        {
            Title = title;
        }

        public bool IsSatisfiedBy(Training training)
        {
            return Title == null || training.Title == Title;
        }
    }
}
