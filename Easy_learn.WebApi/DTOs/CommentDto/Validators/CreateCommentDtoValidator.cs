namespace Easy_learn.WebApi.DTOs.CommentDto.Validators
{
    public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentDtoValidator()
        {
            RuleFor(c=> c.Title).NotEmpty().WithMessage("عنوان نظر اجباری است").NotNull().WithMessage("عنوان نظر اجباری است").MinimumLength(10).WithMessage("عنوان نظر باید بیشتر از 10 نویسه باشد").MaximumLength(100).WithMessage("عنوان نظر نباید بیشتر از 100 نویسه باشد");
        }
    }
}
