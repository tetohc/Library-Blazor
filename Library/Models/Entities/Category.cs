namespace Library.Models.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime RegisterDate { get; set; } = DateTime.Now;
    }
}
