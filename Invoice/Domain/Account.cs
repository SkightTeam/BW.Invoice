namespace Invoice.Domain
{
    public class Account
    {
        public Account(Organization organization)
        {
            Organization = organization;
        }
        protected Account() { }
        public virtual Organization Organization { get; protected set; }
        public virtual int Id { get; protected set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string BankAccountNumber { get; set; }
        public virtual string CurrencyCode { get; set; }
        public virtual string TaxType { get; set; }
    }
}