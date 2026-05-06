namespace Easy_learn.WebApi.DTOs.AnswerQuestionDto.Validators
{
    public class CreateAnswerQuestionDtoValidator : AbstractValidator<CreateAnswerQuestionDto>
    {
        public CreateAnswerQuestionDtoValidator()
        {
            RuleFor(a=> a.QuestionCourseId).NotEmpty().WithMessage("شناسه نمی تواند خالی باشد").NotNull().WithMessage("شناسه نمی تواند خالی باشد");

            RuleFor(a => a.Description).NotEmpty().WithMessage("توضیحات نمی تواند خالی باشد").NotNull().WithMessage("توضیحات نمی تواند خالی باشد").MinimumLength(10).WithMessage("توضیحات باید بیشتر از 10 نویسه باشد").MaximumLength(400).WithMessage("توضیحات باید کمتر از 400 نویسه باشد");
        }
    }
}
