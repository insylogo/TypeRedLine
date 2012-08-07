using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace TypeRacingDao
{
    public static class BaseDao
    {
        private static ISession m_session;
        private static ISessionFactory m_sessionFactory;
        private static Configuration m_config;


        public static ISession Session
        {
            get
            {
                if (m_config == null)
                {
                    m_config = new Configuration().Configure();

                    
                }
                if (m_session == null)
                {
                    m_sessionFactory = m_config.BuildSessionFactory();
                    m_session = m_sessionFactory.OpenSession();
                }
                return m_session;
            }
            private set { }
        }

        public static T Get<T>(int id)
        {
            
            T result = default(T);
            
                result =  Session.Get<T>(id);
             
            return result;
        }

        public static bool SaveOrUpdate<T>(T item)
        {
            bool result = false;
            
            using (var transaction = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(item);
                transaction.Commit();
                result = transaction.WasCommitted;
            }
            return result;
        }
    }
}
