namespace Easy_learn.WebApi.Contracts.Repositories
{
    public interface IUserRepository 
    {
        Task<int?> GetWithIncludeId(string username, string include);
        Task<bool> IsStudent(string userId , int courseId);
    }
}
