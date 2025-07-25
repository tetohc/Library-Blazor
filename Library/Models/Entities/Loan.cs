namespace Library.Models.Entities
{
    public class Loan
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public Guid StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(15);
        public string Status { get; set; } = string.Empty!;
    }
}
