namespace Invoice.Domain
{
    public class Balance
    {
        public virtual int  Id { get; set; }
        public virtual decimal? Outstanding { get; set; }
        public virtual decimal? Overdue { get; set; }
    }
}