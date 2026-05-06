namespace Easy_learn.WebApi.Services.Repositories
{
    public class CategoryRepository : GenericRepository<CategoryEntity>, ICategoryRepository
    {
        private readonly Easy_LearnDbContext _context;
        public CategoryRepository(Easy_LearnDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Delete(int Id)
        {
            await _context.Categories.Where(c => c.Id == Id).ExecuteDeleteAsync();
        }
    }
}
