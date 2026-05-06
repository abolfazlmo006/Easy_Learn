namespace Easy_learn.WebApi.DTOs.CourseDto.Validators
{
    public class UpdateCourseDtoValidator : AbstractValidator<UpdateCourseDto>
    {
        public UpdateCourseDtoValidator() 
        {
            Include(new CreateCourseDtoValidator());
        }
    }
}
