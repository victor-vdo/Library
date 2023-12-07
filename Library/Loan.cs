namespace Library
{
    public class Loan : Model
    {
        public Loan(Guid userId, Guid bookId, DateTime loanDate)
        {
            UserId = userId;
            BookId = bookId;
            LoanDate = loanDate;
        }

        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public bool Active { get; set; }
    }
}
