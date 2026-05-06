namespace Easy_learn.WebApi.DTOs.QuestionCourseDto.Validators
{
    public class CreateQuestionCourseDtoValidator : AbstractValidator<CreateQuestionCourseDto>
    {
        public CreateQuestionCourseDtoValidator()
        {
            RuleFor(q => q.CourseId).NotEmpty().WithMessage("شناسه دوره اجباری است").NotNull().WithMessage("شناسه دوره اجباری است");

            RuleFor(q=> q.Title).NotEmpty().WithMessage("عنوان سوال اجباری است").NotNull().WithMessage("عنوان سوال اجباری است").MinimumLength(10).WithMessage("عنوان سوال باید بیشتر از 10 نویسه باشد").MaximumLength(60).WithMessage("عنوان سوال باید کمتر از 60 نویسه باشد");

            RuleFor(q => q.Description).NotEmpty().WithMessage("توضیحات سوال اجباری است").NotNull().WithMessage("توضیحات سوال اجباری است").MinimumLength(10).WithMessage("توضیحات سوال باید بیشتر از 10 نویسه باشد").MaximumLength(1000).WithMessage("توضیحات سوال باید کمتر از 1000 نویسه باشد");
        }
    }
}
