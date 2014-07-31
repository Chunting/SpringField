using System;
using System.Collections.Generic;
using System.Text;

namespace ResumeCollector.Lib
{
    public class TaskState
    {
        private bool m_IsStep;
        private bool m_NextResume;
        private bool m_IsStop;
        private bool m_IsCancel;

        public TaskState()
        {
            m_NextResume = false;
            m_IsStep = false;
            m_IsStop = false;
            m_IsCancel = false;
        }

        public bool IsStep
        {
            get { return m_IsStep; }
            set { m_IsStep = value; }
        }

        public bool IsStop
        {
            get { return m_IsStop; }
            set { m_IsStop = value; }
        }

        public bool IsCancel
        {
            get { return m_IsCancel; }
            set { m_IsCancel = value; }
        }

        public bool IsNextResume
        {
            get { return m_NextResume; }
            set { m_NextResume = value; }
        }
    }
}
