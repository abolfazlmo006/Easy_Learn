namespace Easy_Learn.Data.Entities
{
    public class RequestForTeacherEntity
    {
        public int Id { get; set; }
        public string Descrption { get; set; }
        public string Address_Resumes { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}
