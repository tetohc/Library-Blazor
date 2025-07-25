namespace Library.Models.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime? ReleaseDate { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public int Quantity { get; set; }

        public Category Category { get; set; }
    }
}
