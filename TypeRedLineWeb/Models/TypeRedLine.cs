using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shared;
using System.Web.Mvc;
using System.Web.Security;
using TypeRacingDao.Entities;

namespace TypeRedLineWeb.Models
{
    public class TypeRedLineModel
    {
        private TimeSpan m_elapsedTime;
        private bool m_counting;
        private DateTime m_countDown;


        public bool IsStarted { get; private set; }

        public List<Player> Players { get; set; } 
        
        public Dictionary<Player, double> Scores { get; set; }

        public string Text { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan CountDown
        {
            get
            {
                if (!m_counting && !IsStarted)
                {
                    m_countDown = DateTime.Now;
                    m_counting = true;
                }
                return DateTime.Now.Subtract(m_countDown);
            }
        }

        public TimeSpan ElapsedTime { 
            get { return DateTime.Now.Subtract(StartTime); }
        }

    }
}