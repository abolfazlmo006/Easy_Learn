namespace Easy_learn.WebApi.Contracts.Services
{
    public interface IFavoriteService
    {
        Task<Response> Add(int courseId , string userName);
        Task<List<string>> GetByUser(string userName);
        Task Delete(int courseId, string userName);
    }
}
