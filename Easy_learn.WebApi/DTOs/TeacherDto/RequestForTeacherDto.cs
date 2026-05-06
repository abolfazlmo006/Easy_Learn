using System.ComponentModel.DataAnnotations;

namespace Easy_learn.WebApi.DTOs.TeacherDto
{
    public class RequestForTeacherDto
    {
        public string Descrption { get; set; }
        [Url]
        public string Address_Resumes { get; set; }
        public string Mobile { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
