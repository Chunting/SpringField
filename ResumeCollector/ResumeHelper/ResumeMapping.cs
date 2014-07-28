using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using ResumeCollector.Lib;

namespace ResumeCollector.ResumeHelper
{
    public static class ResumeMapping
    {
        public const string BASIC_INFO_PATH = "/Applicant/ApplicantBasicInfo";
        public const string EDU_BACKGROUND_PATH = "/Applicant/ApplicantEduBackground";
        public const string RELATED_INFO_PATH = "/Applicant/ApplicantRelatedInfo";

        static private XmlDocument m_Xml;
        static private XmlNode m_BasicInfoNode;
        static private XmlNode m_EduBackgroundNode;
        static private XmlNode m_RelatedInfoNode;

        static ResumeMapping()
        {
            InitResumeMapping();
        }

        private static void InitResumeMapping()
        {
            string fileName = Application.StartupPath + "\\" + AppConfiguration.ResumeMappingFileName;
            m_Xml = new XmlDocument();
            
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(fileName, settings);
            m_Xml.Load(reader);
            m_BasicInfoNode = GetBasicInfoNode();
            m_EduBackgroundNode = GetEduBackgroundNode();
            m_RelatedInfoNode = GetRelatedInfoNode();
        }

        public static XmlNode GetMappingNode(string xpath)
        {
            return m_Xml.SelectSingleNode(xpath);
        }

        public static XmlNodeList GetMappingNodeList(string xpath)
        {
            return m_Xml.SelectNodes(xpath);
        }

        private static XmlNode GetBasicInfoNode()
        {
            return GetMappingNode(BASIC_INFO_PATH);
        }

        private static XmlNode GetEduBackgroundNode()
        {
            return GetMappingNode(EDU_BACKGROUND_PATH);
        }

        private static XmlNode GetRelatedInfoNode()
        {
            return GetMappingNode(RELATED_INFO_PATH);
        }

        public static XmlNode BasicInfoNode
        {
            get { return m_BasicInfoNode; }
        }

        public static XmlNode EduBackgroundNode
        {
            get { return m_EduBackgroundNode; }
        }

        public static XmlNode RelatedInfoNode
        {
            get { return m_RelatedInfoNode; }
        }

        public static List<PropertyStruct> GetBasicInfoPropertyList()
        {
            List<PropertyStruct> list = new List<PropertyStruct>();
            string curPath;
            foreach (XmlNode node in BasicInfoNode.ChildNodes)
            {
                curPath = BASIC_INFO_PATH + "/" + node.Name;
                list.Add(new PropertyStruct(node, curPath));
            }
            return list;
        }

        public static List<PropertyStruct> GetEduBackgroundPropertyList()
        { 
            List<PropertyStruct> list = new List<PropertyStruct>();
            string curPath;
            foreach (XmlNode node in EduBackgroundNode.ChildNodes)
            {
                curPath = EDU_BACKGROUND_PATH + "/" + node.Name;
                list.Add(new PropertyStruct(node, curPath));
            }
            return list;
        }

        public static List<PropertyStruct> GetRelatedInfoPropertyList()
        { 
            List<PropertyStruct> list = new List<PropertyStruct>();
            string curPath;
            foreach (XmlNode node in RelatedInfoNode.ChildNodes)
            {
                curPath = RELATED_INFO_PATH + "/" + node.Name;
                list.Add(new PropertyStruct(node, curPath));
            }
            return list;
        }
    }
}
