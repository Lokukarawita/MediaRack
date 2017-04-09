using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.Common.DAO
{
    public class BaseDAO<T, ID>
    {

        public ISession GetSession()
        {
            return ORM.ORMFactory.Instance.GetSession();
        }

        public T Get(ID id)
        {
            using (var ses = GetSession())
            {
                return (T)ses.Get(typeof(T), id);
            }
        }
        
        public IList<T> Get()
        {
            using (var ses = GetSession())
            {
                return ses.CreateCriteria(typeof(T)).List<T>();
            }
        }

        public IList<T> Get(Expression<Func<T, bool>> predicate)
        {
            using (var ses = GetSession())
            {
                var qry = ses.Query<T>();
                return qry
                    .Where(predicate)
                    .ToList();
            }
        }

        public void Add(T item)
        {
            using (var ses = GetSession())
            {
                var trans = ses.BeginTransaction();
                try
                {
                    ses.SaveOrUpdate(item);
                    trans.Commit();
                }
                catch (Exception)
                {

                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Update(T item)
        {
            using (var ses = GetSession())
            {
                var trans = ses.BeginTransaction();
                try
                {
                    ses.Update(item);
                    trans.Commit();
                }
                catch (Exception)
                {

                    trans.Rollback();
                    throw;
                }
            }
        }

        public void Delete(ID id)
        {
            using (var ses = GetSession())
            {
                var trans = ses.BeginTransaction();
                try
                {
                    var elem = Get(id);
                    ses.Delete(elem);
                    trans.Commit();
                }
                catch (Exception)
                {

                    trans.Rollback();
                    throw;
                }
            }
        }

    }
}
