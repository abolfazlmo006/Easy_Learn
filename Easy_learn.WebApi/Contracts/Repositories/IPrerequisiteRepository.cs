namespace Easy_learn.WebApi.Contracts.Repositories
{
    public interface IPrerequisiteRepository : IGenericRepository<PrerequisiteEntity>
    {
        Task<List<string>> GetPrerequisiteForCourse(int courseId);
        Task DeleteByCourse(int courseId);
    }
}
