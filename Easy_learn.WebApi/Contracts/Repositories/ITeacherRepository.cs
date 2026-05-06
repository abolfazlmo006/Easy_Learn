global using Easy_Learn.Data.Entities;
using Easy_learn.WebApi.DTOs.TeacherDto;

namespace Easy_learn.WebApi.Contracts.Repositories
{
    public interface ITeacherRepository : IGenericRepository<TeacherEntity>
    {
        Task Delete(int Id);
        Task RequestForTeacher(RequestForTeacherEntity entity);
        Task<string> VerifyRequestForTeacher(int RequestForTeacher_Id);
        Task<List<GetRequestForTeacherDto>> GetRequestForTeacher();
    }
}
