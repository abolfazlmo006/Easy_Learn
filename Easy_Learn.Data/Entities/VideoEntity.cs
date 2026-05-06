namespace Easy_Learn.Data.Entities
{
    public class VideoEntity
    {
        public int Id { get; set; }
        public string Address_Video { get; set; }
        public string Title_Video { get; set; }
        public int CourseId { get; set; }
        public CourseEntity Course { get; set; }
        public bool IsSuccess { get; set; }
    }
}
