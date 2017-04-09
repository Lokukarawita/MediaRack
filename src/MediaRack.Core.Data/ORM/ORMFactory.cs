using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRack.Core.Data.ORM
{
    public class ORMFactory
    {
        #region Singleton
        private static ORMFactory _instace;

        public static ORMFactory Instance
        {
            get
            {
                if (_instace == null)
                    _instace = new ORMFactory();
                return _instace;
            }
        }

        #endregion

        private ISessionFactory factory;

        public ORMFactory()
        {
            var fn = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MediaRack.db3");
            factory = Fluently
                .Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(fn))
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<ORMFactory>())
                .BuildSessionFactory();
        }

        public ISession GetSession()
        {
            return this.factory.OpenSession();
        }

        public ISession GetSession(IInterceptor interceptor)
        {
            return this.factory.OpenSession(interceptor);
        }
    }
}
