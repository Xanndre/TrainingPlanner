using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.TrainerCardFilters
{
    public class TrainerCardMatchesName : ISpecification<TrainerCard>
    {
        public string Name { get; set; }
        public TrainerCardMatchesName(string name)
        {
            Name = name;
        }

        public bool IsSatisfiedBy(TrainerCard trainerCard)
        {
            return Name == null || trainerCard.Name == Name;
        }
    }
}
