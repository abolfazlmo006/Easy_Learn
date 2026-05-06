global using FluentValidation;

namespace Easy_learn.WebApi.DTOs.CourseDto.Validators
{
    public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
    {
        public CreateCourseDtoValidator()
        {
            RuleFor(c=> c.Name).NotEmpty().WithMessage("نام دوره اجباری است").NotNull().WithMessage("نام دوره اجباری است").MinimumLength(10).WithMessage("نام دوره باید بیشتر از 10 نویسه باشد").MaximumLength(50).WithMessage("نام دوره نباید بیشتر از 50 نویسه باشد");
            RuleFor(c => c.Short_Description).NotEmpty().WithMessage("توضیحات دوره اجباری است").NotNull().WithMessage("توضیحات دوره اجباری است").MinimumLength(20).WithMessage("توضیحات دوره باید بیشتر از 20 نویسه باشد").MaximumLength(400).WithMessage("توضیحات دوره نباید بیشتر از 400 نویسه باشد");
            RuleFor(c => c.Description).NotEmpty().WithMessage("توضیحات دوره اجباری است").NotNull().WithMessage("توضیحات دوره اجباری است").MinimumLength(100).WithMessage("توضیحات دوره باید بیشتر از 100 نویسه باشد").MaximumLength(3000).WithMessage("توضیحات دوره نباید بیشتر از 3000 نویسه باشد");

            RuleFor(c => c.Price).LessThan(10000000).WithMessage("قیمت دوره نباید بیشتر از 10 میلیون باشد");

            RuleFor(c => c.Status).NotEmpty().WithMessage("وضعیت دوره اجباری است").NotNull().WithMessage("وضعیت دوره اجباری است").MinimumLength(7).WithMessage("وضعیت دوره باید بیشتر از 7 نویسه باشد").MaximumLength(20).WithMessage("وضعیت دوره نباید بیشتر از 20 نویسه باشد");

            RuleFor(c => c.Level_Course).NotEmpty().WithMessage("سطح دوره اجباری است").NotNull().WithMessage("سطح دوره اجباری است").MinimumLength(4).WithMessage("سطح دوره باید بیشتر از 4 نویسه باشد").MaximumLength(20).WithMessage("سطح دوره نباید بیشتر از 20 نویسه باشد");

            RuleFor(c => c.Image).NotEmpty().WithMessage("تصویر دوره الزامی است").NotNull().WithMessage("تصویر دوره الزامی است");

            RuleFor(c=> c.CategoryId).NotEmpty().WithMessage("گروه دوره الزامی است").NotNull().WithMessage("گروه دوره الزامی است");
        }
    }
}
