namespace Easy_Learn.Data.Entities
{
    public class PrerequisiteEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public CourseEntity Course { get; set; }
    }
}
