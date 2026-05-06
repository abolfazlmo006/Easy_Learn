using Easy_learn.WebApi.DTOs.Category;

namespace Easy_learn.WebApi.Contracts.Services
{
    public interface ICategoryService
    {
        Task<Response> Add(CreateCategoryDto dto);
        Task<Response> Update(UpdateCategoryDto dto , int Id);
        Task Delete(int Id);
        Task<List<GetCategoryDto>> GetAll();
    }
}
