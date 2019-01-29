using System;
using System.Collections.Generic;
using System.Text;
using Xero.Api.Core;

namespace Invoice.Integration.Xero
{
    public interface XeroApiQuery<T>: ExternalQuery<T>
    {
        IEnumerable<T> execute(IXeroCoreApi xeroCoreApi);
    }
}
