global using Easy_Learn.UI.Services.Base;
global using Easy_Learn.UI.ViewModels.CategoryVM;
global using Easy_Learn.UI.ViewModels.CommentVM;
global using Easy_Learn.UI.ViewModels.QuestionVM;
global using Easy_Learn.UI.ViewModels.TeacherVM;
global using Easy_Learn.UI.ViewModels.UserVM;
global using Easy_Learn.UI.ViewModels.VideoVM;
using AutoMapper;

namespace Easy_Learn.UI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginDto, LoginVM>().ReverseMap();
            CreateMap<RegisterDto, RegisterVM>().ReverseMap();
            CreateMap<ConfirmEmailDto, ConfirmEmailVM>().ReverseMap();
            CreateMap<ResetPasswordDto, ResetPasswordVM>().ReverseMap();
            CreateMap<GetOrderListDto, GetOrderListVM>().ReverseMap();
            CreateMap<ChangePasswordDto, ChangePasswordVM>().ReverseMap();
            CreateMap<GetAnswerQuestionListDto, GetAnswerQuestionListVM>().ReverseMap();
            CreateMap<GetNotificationDto, GetNotificationVM>().ReverseMap();
            CreateMap<GetOrderDetailDto, GetOrderDetailVM>().ReverseMap();
            CreateMap<CreateVideoDto, CreateVideoVM>().ReverseMap();
            CreateMap<VideosDto, VideosVM>().ReverseMap();
            CreateMap<GetRequestForTeacherDto, GetRequestForTeacherVM>().ReverseMap();
            CreateMap<RequestForTeacherDto, RequestForTeacherVM>().ReverseMap();
            CreateMap<CreateQuestionCourseDto, CreateQuestionCourseVM>().ReverseMap();
            CreateMap<GetQuestionCourseDetailDto, GetQuestionCourseDetailVM>().ReverseMap();
            CreateMap<GetQuestionCourseListDto, GetQuestionCourseListVM>().ReverseMap();
            CreateMap<UpdateQuestionCourseDto, UpdateQuestionCourseVM>().ReverseMap();
            CreateMap<CourseDetailDto, CourseDetailVM>().ReverseMap();
            CreateMap<CourseListDto, CourseListVM>().ReverseMap();
            CreateMap<CreateCourseDto, CreateCourseVM>().ReverseMap();
            CreateMap<UpdateCourseDto, UpdateCourseVM>().ReverseMap();
            CreateMap<CommentsDto, CommentsVM>().ReverseMap();
            CreateMap<UpdateCommentDto, UpdateCommentVM>().ReverseMap();
            CreateMap<CreateCommentDto, CreateCommentVM>().ReverseMap();
            CreateMap<CreateCategoryDto, CreateCategoryVM>().ReverseMap();
            CreateMap<GetCategoryDto, GetCategoryVM>().ReverseMap();
            CreateMap<UpdateCategoryDto, UpdateCategoryVM>().ReverseMap();
            CreateMap<CreateAnswerQuestionDto, CreateAnswerQuestionVM>().ReverseMap();
            CreateMap<UpdateAnswerQuestionDto, UpdateAnswerQuestionVM>().ReverseMap();
            CreateMap<ProfileUserVM,ProfileUserDto>().ReverseMap();
        }
    }
}
