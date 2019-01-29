using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Persistent
{
    public interface Repository
    {
        IEnumerable<T> search<T>(Query<T> query);
        void create<T>(T entity);
        void update<T>(T entity);
        void delete<T>(T entity);
    }
}
