using System;
using System.Collections.Generic;
using System.Text;
using Invoice.Domain;

namespace Invoice.Integration
{
    public interface ExternalRepository
    {
        T Get<T>(ExternalQuery<T> query);
        IEnumerable<T> Search<T>(ExternalQuery<T> query);
    }
}
