using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

namespace TypeRacingDao
{
    public abstract class BaseDao<T>
    {
        private static ISession m_session;
        private static ISessionFactory m_sessionFactory;
        private static Configuration m_config;


        /// <summary>
        /// Gets the session.
        /// </summary>
        public static ISession Session
        {
            get
            {
                try
                {
                    if (m_config == null)
                    {
                        m_config = new Configuration().Configure();
                    }
                    if (m_sessionFactory == null)
                    {
                        m_sessionFactory = m_config.BuildSessionFactory();
                    }
                    if (m_session == null)
                    {
                        m_session = m_sessionFactory.OpenSession();
                    }
                } catch (Exception e)
                {
                    throw new Exception("Connection failed.", e);
                }
                return m_session;
            }
        }

        /// <summary>
        /// Gets the entity by specified id.
        /// </summary>
        /// <typeparam name="T">type of the entity</typeparam>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static T Get(int id)
        {
            
            T result = default(T);
            result =  Session.Get<T>(id);
             
            return result;
        }

        /// <summary>
        /// Gets all of specified type.
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <returns></returns>
        public static IList<T> GetAll ()
        {
            return GetByCriteria();
        }

        /// <summary>
        /// Gets the entity by criteria.
        /// </summary>
        /// <typeparam name="T">type of the entity</typeparam>
        /// <param name="criterion">The criterion.</param>
        /// <returns></returns>
        public static IList<T> GetByCriteria(params ICriterion[] criterion)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));
            foreach (var criterium in criterion)
            {
                criteria.Add(criterium);
            }

            return criteria.List<T>();
        }

        /// <summary>
        /// Gets the entity by example.
        /// </summary>
        /// <typeparam name="T">type of the entity</typeparam>
        /// <param name="exampleInstance">The example instance.</param>
        /// <param name="propertiesToExclude">The properties to exclude.</param>
        /// <returns></returns>
        public static IList<T> GetByExample(T exampleInstance, string[] propertiesToExclude)
        {
            ICriteria criteria = Session.CreateCriteria(typeof (T));
            Example example = Example.Create(exampleInstance);
            foreach (var excludedProperty in propertiesToExclude)
            {
                example.ExcludeProperty(excludedProperty);
            }
            criteria.Add(example);

            return criteria.List<T>();
        }
        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <typeparam name="T">type of entity</typeparam>
        /// <param name="id">The id.</param>
        /// <param name="shouldLock">if set to <c>true</c> [should lock].</param>
        /// <returns></returns>
        public T GetById(int id, bool shouldLock)
        {

            T entity;

            if (shouldLock)
            {
                entity = (T)Session.Load(typeof(T), id, LockMode.Upgrade);
            }

            else
            {
                entity = (T) Session.Load(typeof (T), id);
            }
            return entity;
        }


        /// <summary>
        /// Saves or updates the specified item.
        /// </summary>
        /// <typeparam name="T">type of the item</typeparam>
        /// <param name="item">The item.</param>
        /// <returns>The item specified.</returns>
        public static T SaveOrUpdate(T item)
        {
            using (var tx = Session.BeginTransaction())
            {
                Session.SaveOrUpdate(item);
                tx.Commit();
            }
            
            return item;
        }

        public static void Delete(T item)
        {
            Session.Delete(item);
        }

    }
}
