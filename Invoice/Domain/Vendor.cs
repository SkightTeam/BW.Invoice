using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain
{
    public class Vendor
    {
        public Vendor(Organization organization)
        {
            Organization = organization;
        }
        protected Vendor() { }
        public virtual Organization Organization { get; protected set; }
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string ContactNumber { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string BankAccountDetails { get; set; }
        public virtual string TaxNumber { get; set; }
        public virtual string PurchaseAccountCode { get; set; }       
        public virtual string AccountNumber { get; set; }
    }
}
