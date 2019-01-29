using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Criterion;

namespace Invoice.Persistent.NHibernate
{
    public interface NHibernateQuery<T> : Query<T>
    {
        QueryOver<T> Query { get; }
    }
}
