namespace Easy_learn.WebApi.Contracts.Repositories
{
    public interface IFavoriteRepository : IGenericRepository<FavoriteEntity>
    {
        Task<List<string>> GetByUser(string userId);
        Task Delete(string userId , int courseId);
    }
}
