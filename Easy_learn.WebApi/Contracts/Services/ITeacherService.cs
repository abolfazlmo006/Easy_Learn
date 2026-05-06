using Easy_learn.WebApi.DTOs.TeacherDto;

namespace Easy_learn.WebApi.Contracts.Services
{
    public interface ITeacherService
    {
        Task Delete(int Id);
        Task<Response> RequestForTeacher(RequestForTeacherDto dto , string UserName);
        Task VerifyRequestForTeacher(int RequestForTeacher_Id);
        Task<List<GetRequestForTeacherDto>> GetRequestForTeacher();
    }
}
