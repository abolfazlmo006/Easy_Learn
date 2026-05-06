namespace Easy_learn.WebApi.DTOs.VideoDto.Validators
{
    public class CreateVideoDtoValidator : AbstractValidator<CreateVideoDto>
    {
        public CreateVideoDtoValidator() 
        {
            RuleFor(v=> v.Address_Video).NotEmpty().WithMessage("ویدیو اجباری است").NotNull().WithMessage("ویدیو اجباری است");

            RuleFor(v=> v.Title_Video).NotEmpty().WithMessage("عنوان ویدیو اجباری است").NotNull().WithMessage("عنوان ویدیو اجباری است").MinimumLength(5).WithMessage("عنوان ویدیو باید بیشتر از 5 نویسه باشد").MaximumLength(50).WithMessage("عنوان ویدیو نباید بیشتر از 50 نویسه باشد");

        }
    }
}
