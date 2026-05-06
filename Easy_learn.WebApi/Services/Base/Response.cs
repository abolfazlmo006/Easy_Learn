namespace Easy_learn.WebApi.Services.Base
{
    public class Response
    {
        public List<string>? errors { get; set; }
        public string Message { get; set; }
        public bool SuccessFul { get; set; }
        public string? Data { get; set; }
    }
    public class LoginResponse:Response
    {
        public string? FullName { get; set; }
    }
}
