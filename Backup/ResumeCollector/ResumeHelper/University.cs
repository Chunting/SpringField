using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ResumeCollector.Lib;
using System.Windows.Forms;

namespace ResumeCollector.ResumeHelper
{
    public static class University
    {
        public const string UNVISITY_LIST_PATH = "/resource/universities";
        public const string NAME = "name";

        static private XmlDocument m_Xml;
        static private XmlNode m_Universities;
        static private List<string> m_ContinentList;
        static private List<string> m_CountryList;

        static University()
        {
            InitList();
        }

        private static void InitList()
        {
            try
            {
                m_ContinentList = new List<string>();
                m_CountryList = new List<string>();

                string fileName = Application.StartupPath + "\\" + AppConfiguration.ProjectResourceFileName;
                m_Xml = new XmlDocument();

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                XmlReader reader = XmlReader.Create(fileName, settings);
                m_Xml.Load(reader);

                m_Universities = m_Xml.SelectSingleNode(UNVISITY_LIST_PATH);

                foreach (XmlNode ContinentNode in Universities.ChildNodes)
                {
                    m_ContinentList.Add(ContinentNode.Attributes[NAME].Value);
                    foreach (XmlNode CountryNode in ContinentNode.ChildNodes)
                    {
                        if (ContinentNode.Attributes[NAME].Value != "Europe")
                        {
                            m_CountryList.Add(CountryNode.Attributes[NAME].Value);
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public static bool isInContinentList(string ContinentName)
        {
            if (m_ContinentList != null && m_ContinentList.IndexOf(ContinentName) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isInCountryList(string CountryName)
        {
            if (m_CountryList != null && m_CountryList.IndexOf(CountryName) > -1)
                return true;
            else
                return false;
        }

        public static XmlNode Universities
        {
            get { return m_Universities; }
        }
        public static List<string> getContinentList
        {
            get { return m_ContinentList; }
        }
        public static List<string> getCountryList
        {
            get { return m_CountryList; }
        }
    }
}
