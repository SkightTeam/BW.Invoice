using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain
{
    public class Organization
    {
        public virtual int Id { get; set; }
        public virtual string ShortCode { get; set; }
        public virtual string Name { get; set; }
        public virtual string  LegalName { get; set; }
    }
}
