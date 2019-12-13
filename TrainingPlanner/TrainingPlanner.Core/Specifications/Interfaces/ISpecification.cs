namespace TrainingPlanner.Core.Specifications.Interfaces
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
    }
}
