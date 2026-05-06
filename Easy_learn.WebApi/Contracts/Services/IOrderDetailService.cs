namespace Easy_learn.WebApi.Contracts.Services
{
    public interface IOrderDetailService
    {
        Task Delete(int id);
        Task<Response> Add(int CourseId , string UserName);
    }
}
