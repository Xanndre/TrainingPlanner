using TrainingPlanner.Data;

namespace TrainingPlanner.Repositories.Repositories
{
    public class BaseRepository
    {
        protected TrainingPlannerDbContext _trainingPlannerDbContext;
        public BaseRepository(TrainingPlannerDbContext dbContext)
        {
            _trainingPlannerDbContext = dbContext;
        }

    }
}
