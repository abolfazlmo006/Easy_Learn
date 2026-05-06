namespace Easy_learn.WebApi.Contracts.Repositories
{
    public interface ICategoryRepository : IGenericRepository<CategoryEntity>
    {
        Task Delete(int Id);
    }
}
