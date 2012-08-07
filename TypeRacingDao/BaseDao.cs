using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace TypeRacingDao
{
    class BaseDao
    {
        private ISession m_session;
        private Configuration m_config;


        public ISession session { 
            get
            {
                if (m_config == null)
                {
                    m_config = new Configuration().AddAssembly("TypeRacing");

                }
                if (m_session == null)
                {
                    //m_session = 
                }
            } 
        set
        {
            
        }
        }
    }
}
