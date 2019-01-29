using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Transform;

namespace Invoice.Persistent.NHibernate
{
    public class NHibernateRepository : Repository
    {
        private ISession session;

        public NHibernateRepository(ISession session)
        {
            this.session = session;
        }

        public IEnumerable<T> search<T>(Query<T> query)
        {
            var my_query = (query as NHibernateQuery<T>).Query;
            var result = my_query
                .GetExecutableQueryOver(session)
                .TransformUsing(Transformers.DistinctRootEntity).List<T>();
            return result;
        }

        public void create<T>(T entity)
        {
            session.Save(entity);
        }

        public void update<T>(T entity)
        {
            session.Update(entity);
        }

        public void delete<T>(T entity)
        {
            session.Delete(entity);
        }
    }
}
