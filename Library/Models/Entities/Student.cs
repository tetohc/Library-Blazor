namespace Library.Models.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
    }
}
