using TrainingPlanner.Core.Specifications.Interfaces;
using TrainingPlanner.Data.Entities;

namespace TrainingPlanner.Core.Specifications.Filters.TrainerFilters
{
    public class TrainerMatchesKeywords : ISpecification<Trainer>
    {
        public string Keywords { get; set; }
        public TrainerMatchesKeywords(string keywords)
        {
            Keywords = keywords;
        }

        public bool IsSatisfiedBy(Trainer trainer)
        {
            return Keywords == null ||
                trainer.Description.ToLower()
                    .Contains(Keywords.ToLower()) ||
                (trainer.User.FirstName.ToLower() + ' ' + trainer.User.LastName.ToLower())
                    .Contains(Keywords.ToLower());
        }
    }
}
