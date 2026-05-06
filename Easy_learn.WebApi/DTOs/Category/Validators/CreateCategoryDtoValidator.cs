namespace Easy_learn.WebApi.DTOs.Category.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage("عنوان دسته بندی نباید خالی باشد").NotNull().WithMessage("عنوان دسته بندی نباید خالی باشد").MinimumLength(2).WithMessage("عنوان دسته بندی رباید بیشتر از 2 نویسه باشد").MaximumLength(20).WithMessage("عنوان دسته بندی باید کمتر از 20 نویسه باشد");

            RuleFor(c => c.Description).NotEmpty().WithMessage("توضیحات دسته بندی نباید خالی باشد").NotNull().WithMessage("توضیحات دسته بندی نباید خالی باشد").MinimumLength(30).WithMessage("توضیحات دسته بندی رباید بیشتر از 30 نویسه باشد").MaximumLength(300).WithMessage("توضیحات دسته بندی باید کمتر از 20 نویسه باشد");

        }
    }
}
