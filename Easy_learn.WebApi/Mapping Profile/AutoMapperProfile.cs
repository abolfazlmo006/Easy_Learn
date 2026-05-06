global using AutoMapper;
using Easy_learn.WebApi.DTOs.AnswerQuestionDto;
using Easy_learn.WebApi.DTOs.Category;
using Easy_learn.WebApi.DTOs.CommentDto;
using Easy_learn.WebApi.DTOs.QuestionCourseDto;
using Easy_learn.WebApi.DTOs.TeacherDto;
using Easy_learn.WebApi.DTOs.VideoDto;

namespace Easy_learn.WebApi.Mapping_Profile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDto, UserEntity>().ReverseMap();
            CreateMap<CreateCourseDto, CourseEntity>().ReverseMap();
            CreateMap<UpdateCourseDto, CourseEntity>().ReverseMap();
            CreateMap<RequestForTeacherEntity, RequestForTeacherDto>().ReverseMap();
            CreateMap<CreateCommentDto, CommentEntity>().ReverseMap();
            CreateMap<CreateCategoryDto, CategoryEntity>().ReverseMap();
            CreateMap<GetCategoryDto, CategoryEntity>().ReverseMap();
            CreateMap<CreateVideoDto, VideoEntity>().ReverseMap();
            CreateMap<VideosDto,VideoEntity>().ReverseMap();
            CreateMap<CreateQuestionCourseDto, QuestionCourseEntity>().ReverseMap();
            CreateMap<UpdateQuestionCourseDto, QuestionCourseEntity>().ReverseMap();
            CreateMap<CreateAnswerQuestionDto, AnswerQuestionEntity>().ReverseMap();
            
        }
    }
}
