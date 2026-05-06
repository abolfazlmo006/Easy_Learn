namespace Easy_learn.WebApi.DTOs.Category.Validators
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            Include(new CreateCategoryDtoValidator());
        }
    }
}
