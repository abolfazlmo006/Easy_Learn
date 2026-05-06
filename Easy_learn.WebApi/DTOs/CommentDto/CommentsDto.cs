namespace Easy_learn.WebApi.DTOs.CommentDto
{
    public class CommentsDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Full_Name { get; set; }
        public bool Private { get; set; }
        public bool IsSuccess { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
    }
}
