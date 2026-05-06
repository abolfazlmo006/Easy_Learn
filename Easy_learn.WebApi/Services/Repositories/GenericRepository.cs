namespace Easy_learn.WebApi.Services.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly Easy_LearnDbContext _context;

        public GenericRepository(Easy_LearnDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var user = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return user.Entity;
        }

        public async Task<List<TEntity>> GetAll()
        {
            var Entities = await _context.Set<TEntity>().ToListAsync();
            return Entities;
        }

        public async Task<TEntity> GetById(int id)
        {
            var Entity = await _context.Set<TEntity>().FindAsync(id);
            return Entity;
        }

        public async Task Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
