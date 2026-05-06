using Easy_learn.WebApi.DTOs.TeacherDto;
using Easy_learn.WebApi.DTOs.TeacherDto.Validators;

namespace Easy_learn.WebApi.Services.Repositories
{
    public class TeacherRepository : GenericRepository<TeacherEntity>, ITeacherRepository
    {
        private readonly Easy_LearnDbContext _context;
        public TeacherRepository(Easy_LearnDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Delete(int Id)
        {
            await _context.Teachers.Where(t=> t.Id == Id).ExecuteDeleteAsync();
        }

        public async Task<List<GetRequestForTeacherDto>> GetRequestForTeacher()
        {
            var entity = await _context.RequestForTeachers.Select(r => new GetRequestForTeacherDto
            {
                Id = r.Id,
                Address_Resumes = r.Address_Resumes,
                Descrption = r.Descrption,
                Email = r.Email,
                Mobile = r.Mobile
            }).AsNoTrackingWithIdentityResolution().ToListAsync();

            return entity;
        }

        public async Task RequestForTeacher(RequestForTeacherEntity entity)
        {
            await _context.RequestForTeachers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<string> VerifyRequestForTeacher(int RequestForTeacher_Id)
        {
            var re = await _context.RequestForTeachers.FindAsync(RequestForTeacher_Id);

            var UserId = re.UserId;

            await Add(new TeacherEntity()
            {
                UserId = UserId,
            });
            
            
            _context.RequestForTeachers.Remove(re);
            await _context.SaveChangesAsync();
            return UserId;
        }
    }
}
