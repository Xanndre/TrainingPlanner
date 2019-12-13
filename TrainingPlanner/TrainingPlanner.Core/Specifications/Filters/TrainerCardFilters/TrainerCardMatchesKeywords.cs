using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.TrainerCardFilters
{
    public class TrainerCardMatchesKeywords : ISpecification<TrainerCard>
    {
        public string Keywords { get; set; }
        public TrainerCardMatchesKeywords(string keywords)
        {
            Keywords = keywords;
        }

        public bool IsSatisfiedBy(TrainerCard trainerCard)
        {
            return Keywords == null ||
                trainerCard.UserName.ToLower().Contains(Keywords.ToLower()) ||
                trainerCard.TrainerName.ToLower().Contains(Keywords.ToLower());
        }
    }
}
