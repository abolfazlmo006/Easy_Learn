global using Easy_learn.WebApi.Contracts.Repositories;

namespace Easy_learn.WebApi.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Easy_LearnDbContext _context;

        public UserRepository(Easy_LearnDbContext context)
        {
            _context = context;
        }

        public async Task<int?> GetWithIncludeId(string username, string include)
        {
            switch (include)
            {
                case "Teacher":
                    var u = await _context.Users.Where(u=> u.UserName == username).Select(u=> u.Teacher.Id)
                      .FirstOrDefaultAsync();
                    return u;
                case "RequestForTeacher":
                    var u3 = await _context.Users.Select(u => new
                    {
                        u.RequestForTeacher.Id,
                        u.UserName
                    }).FirstOrDefaultAsync(u => u.UserName == username);
                    return u3.Id;
                default:
                    return null;
            }
        }

        public async Task<bool> IsStudent(string userId, int courseId)
        {

            var result = await _context.CourseEntityUserEntities.FirstOrDefaultAsync(o=> o.CourseId == courseId && o.UserId == userId);

            return result == null ? false : true;
        }
    }
}
