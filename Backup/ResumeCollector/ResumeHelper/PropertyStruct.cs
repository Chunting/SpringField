using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Xml;
using ResumeCollector.CustomExceptions;

namespace ResumeCollector.ResumeHelper
{
    public class PropertyStruct
    {
        private const string DATA_TYPE = "type";
        private const string DATA_VALUE = "value";
        private const string MAPPING_NAME = "mapping";
        private const string CONTROL = "control";
        private const string REQUIRED = "required";

        private string m_PropertyName;
        private string m_DataType;
        private string m_DataValue;
        private string m_MappingName;
        private string m_Control;
        private bool m_Required;
        private string m_Xpath;
        private NameValueCollection m_Attributes;

        public PropertyStruct(XmlNode node, string xpath)
        {
            m_Xpath = xpath;
            XmlAttributeCollection attributeCollection = node.Attributes;
            m_PropertyName = node.Name;
            try
            {
                m_DataType = attributeCollection[DATA_TYPE].Value;
            }
            catch {
                new PropertyNotExistExceptions(DATA_TYPE);
            }

            try
            {
                m_Control = attributeCollection[CONTROL].Value;
            }
            catch {
                new PropertyNotExistExceptions(CONTROL);
            }

            if (attributeCollection[REQUIRED] != null)
            {
                m_Required = Convert.ToBoolean(attributeCollection[REQUIRED].Value);
            }
            else
            {
                m_Required = true;
            }

            if(attributeCollection[MAPPING_NAME] != null)
            {
                m_MappingName = attributeCollection[MAPPING_NAME].Value;
            }
            else
            {
                //new PropertyNotExistExceptions(MAPPING_NAME);
                m_MappingName = string.Empty;
            }

            if (attributeCollection[DATA_VALUE] != null)
            {
                m_DataValue = attributeCollection[DATA_VALUE].Value;
            }
            else
            {
                m_DataValue = string.Empty;
            }

            m_Attributes = new NameValueCollection();
            foreach (XmlAttribute attribute in attributeCollection)
            {
                m_Attributes.Add(attribute.Name, attribute.Value);
            }
        }

        public string PropertyName
        {
            get { return m_PropertyName; }
        }

        public string DataType
        {
            get { return m_DataType; }
        }

        public string DataValue
        {
            get { return m_DataValue; }
        }

        public string MappingName
        {
            get { return m_MappingName; }
        }

        public string Control
        {
            get { return m_Control; }
        }

        public NameValueCollection Attributes
        {
            get { return m_Attributes; }
        }

        public bool Required
        {
            get { return m_Required; }
        }

        public string XPath
        {
            get { return m_Xpath; }
        }
    }
}
