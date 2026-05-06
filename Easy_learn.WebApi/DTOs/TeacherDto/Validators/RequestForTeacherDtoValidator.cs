namespace Easy_learn.WebApi.DTOs.TeacherDto.Validators
{
    public class RequestForTeacherDtoValidator : AbstractValidator<RequestForTeacherDto>
    {
        public RequestForTeacherDtoValidator()
        {
            RuleFor(r => r.Email).NotEmpty().WithMessage("ایمیل اجباری است").NotNull().WithMessage("ایمیل اجباری است").MinimumLength(10).WithMessage("ایمیل باید بیشتر از 10 نویسه باشد").MaximumLength(60).WithMessage("ایمیل نباید بیشتر از 60 نویسه باشد");

            RuleFor(r => r.Address_Resumes).NotEmpty().WithMessage("رزومه اجباری است").NotNull().WithMessage("رزومه اجباری است");

            RuleFor(r => r.Mobile).NotEmpty().WithMessage("شماره موبایل اجباری است").NotNull().WithMessage("شماره موبایل اجباری است").MinimumLength(11).WithMessage("شماره موبایل باید 11 رقمی باشد").MaximumLength(11).WithMessage("شماره موبایل باید 11 رقمی باشد");

            RuleFor(r => r.Descrption).NotEmpty().WithMessage("توضیحات اجباری است").NotNull().WithMessage("توضیحات اجباری است").MinimumLength(10).WithMessage("توضیحات باید بیشتر از 10 نویسه باشد").MaximumLength(3000).WithMessage("توضیحات نباید بیشتر از 3000 نویسه باشد");

        }
    }
}
