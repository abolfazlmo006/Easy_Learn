namespace Easy_learn.WebApi.Services.Repositories
{
    public class PrerequisiteRepository : GenericRepository<PrerequisiteEntity>, IPrerequisiteRepository
    {
        private readonly Easy_LearnDbContext _context;
        public PrerequisiteRepository(Easy_LearnDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<string>> GetPrerequisiteForCourse(int courseId)
        {
            var prerequisites = await _context.Prerequisites.Where(p=> p.CourseId == courseId).Select(p=> p.Name).ToListAsync();
            return prerequisites;
        }
        public async Task DeleteByCourse(int courseId)
        {
            await _context.Prerequisites.Where(p => p.CourseId == courseId).ExecuteDeleteAsync();
        }
    }
}
