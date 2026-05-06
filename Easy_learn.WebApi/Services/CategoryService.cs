using Easy_learn.WebApi.DTOs.Category;
using Easy_learn.WebApi.DTOs.Category.Validators;

namespace Easy_learn.WebApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMessageSender _messageSender;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IMessageSender messageSender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messageSender = messageSender;
        }

        public async Task<Response> Add(CreateCategoryDto dto)
        {
            var response = new Response();
            var validator = new CreateCategoryDtoValidator();
            var validationresult = await validator.ValidateAsync(dto);
            if (!validationresult.IsValid)
            {
                response.errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();
                response.Message = "عملیات افزودن دسته بندی با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }

            try
            {
                var map = _mapper.Map<CategoryEntity>(dto);
                await _unitOfWork.Category.Add(map);
            }
            catch (Exception e)
            {
                response.Message = "عملیات افزودن دسته بندی با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }

            response.Message = "عملیات افزودن دسته بندی با موفقیت انجام شد";
            response.SuccessFul = true;
            return response;
        }

        public async Task Delete(int Id)
        {
            await _unitOfWork.Category.Delete(Id);
        }

        public async Task<List<GetCategoryDto>> GetAll()
        {
            var categories = await _unitOfWork.Category.GetAll();
            var map = _mapper.Map<List<GetCategoryDto>>(categories);
            return map;
        }

        public async Task<Response> Update(UpdateCategoryDto dto , int Id)
        {
            var response = new Response();
            var validator = new UpdateCategoryDtoValidator();
            var validationresult = await validator.ValidateAsync(dto);
            if (!validationresult.IsValid)
            {
                response.errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();
                response.Message = "عملیات ویرایش دسته بندی با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }

            try
            {
                var category = await _unitOfWork.Category.GetById(Id);
                if (category == null)
                {
                    response.Message = "عملیات ویرایش دسته بندی با شکست مواجه شد";
                    response.SuccessFul = false;
                    return response;
                }
                category.Title = dto.Title;
                category.Description = dto.Description;
                await _unitOfWork.Category.Update(category);
            }
            catch (Exception e)
            {
                response.Message = "عملیات ویرایش دسته بندی با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }

            response.Message = "عملیات ویرایش دسته بندی با موفقیت انجام شد";
            response.SuccessFul = true;
            return response;
        }
    }
}
