using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace ResumeCollector.Lib
{
    //singleton
    public static class TaskLog
    {
        //private static TaskLog curLog;
        private static XmlDocument m_TaskLog;
        private static StringBuilder m_LogBuilder;
        private static XmlElement m_Root;

        private const string LOG_ROOT = "log";
        public const string DATE_TIME = "time";
        public const string RESUME = "resume";
        public const string EXCEPTION = "exception";
        public const string ITEM = "item";
        public const string ACTION = "action";

        static TaskLog()
        {
            Clear();
        }

        private static XmlElement CreateBase()
        {
            XmlDeclaration xmldecl = m_TaskLog.CreateXmlDeclaration("1.0", "utf-8", string.Empty);
            m_TaskLog.AppendChild(xmldecl);
            XmlElement rootElement = m_TaskLog.CreateElement(LOG_ROOT);
            m_TaskLog.AppendChild(rootElement);
            return m_TaskLog.DocumentElement;
        }

        //public static TaskLog GetInstance()
        //{
        //    if (curLog == null)
        //    {
        //        curLog = new TaskLog();
        //    }
        //    return curLog;
        //}

        //protected void RenderHeader()
        //{ 
            
        //}

        //protected void RenderFooter()
        //{ 
            
        //}

        public static XmlElement AddNewEntry(XmlElement parent, string entryName, string content)
        {
            XmlElement curEntry = m_TaskLog.CreateElement(entryName);
            curEntry.SetAttribute(DATE_TIME, DateTime.Now.ToLongTimeString());
            curEntry.InnerText = content;
            parent.AppendChild(curEntry);
            m_LogBuilder.Append("Time:");
            m_LogBuilder.AppendLine(DateTime.Now.ToString());
            m_LogBuilder.Append("Content:");
            m_LogBuilder.AppendLine(content); return curEntry;
        }

        public static XmlElement AddGlobalEntry(string entryName, string content)
        { 
            XmlElement curEntry = m_TaskLog.CreateElement(entryName);
            curEntry.SetAttribute(DATE_TIME, DateTime.Now.ToString());
            curEntry.InnerText = content;
            m_Root.AppendChild(curEntry);
            m_LogBuilder.Append("Time:");
            m_LogBuilder.AppendLine(DateTime.Now.ToString());
            m_LogBuilder.Append("Content:");
            m_LogBuilder.AppendLine(content);
            return curEntry;
        }

        public static void Clear()
        {
            m_TaskLog = new XmlDocument();
            m_LogBuilder = new StringBuilder();
            m_Root = CreateBase();
        }

        public static void SaveAs(string fileName)
        {
            string dir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir))
            {
                try
                {
                    Directory.CreateDirectory(dir);
                }
                catch(System.Exception ex)
                {
                    throw ex;
                }
            }
            m_TaskLog.Save(fileName);
        }

        public static XmlDocument XML
        {
            get { return m_TaskLog; }
        }

        public static string ToXMLString()
        {
            return m_TaskLog.OuterXml;
        }

        public static string ToPureString()
        {
            return m_LogBuilder.ToString();
        }

        public static XmlElement RootElement
        {
            get { return m_Root; }
        }
    }
}
