using Easy_learn.WebApi.DTOs.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easy_learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("Get")]
        [AllowAnonymous]
        public async Task<ActionResult<List<GetCategoryDto>>> Get()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Add(CreateCategoryDto dto)
        {
            var result = await _categoryService.Add(dto);
            return Ok(result);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Response>> Update(UpdateCategoryDto dto , int Id)
        {
            var result = await _categoryService.Update(dto, Id);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            await _categoryService.Delete(Id);
            return NoContent();
        }
    }
}
