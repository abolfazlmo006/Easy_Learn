namespace Easy_Learn.UI.Services
{
    public class Response<T>
    {
        public string Massage { get; set; }
        public string Errors { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
