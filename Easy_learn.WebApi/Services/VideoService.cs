 using Easy_learn.WebApi.DTOs.VideoDto;
using Easy_learn.WebApi.DTOs.VideoDto.Validators;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Easy_learn.WebApi.Services
{
    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VideoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Add(CreateVideoDto dto , int CourseId)
        {
            var response = new Response();
            var validate = new CreateVideoDtoValidator();
            var validationresult = await validate.ValidateAsync(dto);
            if (!validationresult.IsValid)
            {
                response.errors = validationresult.Errors.Select(e=> e.ErrorMessage).ToList();
                response.Message = "عملیات افزودن ویدیو با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }

            var course = await _unitOfWork.Course.GetById(CourseId);
            course.UpdateTime = DateTime.Now;
            await _unitOfWork.Course.Update(course);

            var video = _mapper.Map<VideoEntity>(dto);
            video.CourseId = CourseId;
            await _unitOfWork.Video.Add(video);
            

            response.Message = "عملیات افزودن ویدیو با موفقیت انجام شد و بعد از بررسی ویدیو توسط ادمین قرار میگیرد";
            response.SuccessFul = true;
            return response;
        }

        public async Task Delete(int Id)
        {
            var video = await _unitOfWork.Video.GetById(Id);

            var course = await _unitOfWork.Course.GetById(video.CourseId);
            course.UpdateTime = DateTime.Now;
            await _unitOfWork.Course.Update(course);

            await _unitOfWork.Video.Delete(Id);
        }

        public async Task<List<VideosDto>> GetByAdmin()
        {
            var videos = await _unitOfWork.Video.GetVideosByAdmin();
            var map = _mapper.Map<List<VideosDto>>(videos);
            return map;
        }

        public async Task<List<VideosDto>> GetByCourse(int CourseId)
        {
            var videos = await _unitOfWork.Video.GetVideosByCourse(CourseId);
            var map = _mapper.Map<List<VideosDto>>(videos);
            return map;
        }

        public async Task<Response> Update(CreateVideoDto dto, int Id)
        {
            var response = new Response();
            var validate = new CreateVideoDtoValidator();
            var validationresult = await validate.ValidateAsync(dto);
            if (!validationresult.IsValid)
            {
                response.errors = validationresult.Errors.Select(e => e.ErrorMessage).ToList();
                response.Message = "عملیات ویرایش ویدیو با شکست مواجه شد";
                response.SuccessFul = false;
                return response;
            }

            var video = await _unitOfWork.Video.GetById(Id);
            

            if (video == null)
            {
                response.Message = "ویدیوی وجود ندارد";
                response.SuccessFul = false;
                return response;
            }

            var course = await _unitOfWork.Course.GetById(video.CourseId);
            course.UpdateTime = DateTime.Now;
            await _unitOfWork.Course.Update(course);

            _mapper.Map(dto, video);
            await _unitOfWork.Video.Update(video);


            response.Message = "عملیات ویرایش ویدیو با موفقیت انجام شد و بعد از بررسی ویدیو توسط ادمین قرار میگیرد";
            response.SuccessFul = true;
            return response;
        }

        public async Task Verify(int Id)
        {
            await _unitOfWork.Video.Verify(Id);
        }
    }
}
