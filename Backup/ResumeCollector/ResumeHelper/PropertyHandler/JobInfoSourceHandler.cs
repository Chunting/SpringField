using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ResumeCollector.ResumeHelper;
using ResumeCollector.CustomExceptions;

namespace ResumeCollector.ResumeHelper
{
    public class JobInfoSourceHandler : IPropertyHandler
    {

        #region IPropertyHandler Members

        private const string ITEM = "Item";
        private const string VALUE = "value";
        private const string MAPPING = "mapping";

        public void PopulateProperty(System.Reflection.PropertyInfo propertyInfo, PropertyStruct propertyStruct, ResumeScheme resumeScheme, object parentObj)
        {
            XmlNodeList itemList;

            string itemPath = propertyStruct.XPath + "/" + ITEM;

            itemList = ResumeMapping.GetMappingNodeList(itemPath);

            foreach (XmlNode node in itemList)
            {
                if (resumeScheme.GetOptionButtonValue(1, node.Attributes[MAPPING].Value))
                {
                    propertyInfo.SetValue(parentObj, node.Attributes[VALUE].Value,null);
                    return;
                }
            }

            // code runs here means the applicant miss the rank field in the applyform
            // We will fill it with the defult values;
            string defaultValue = "Microsoft Student Technical Club";
            propertyInfo.SetValue(parentObj, defaultValue, null);
        }

        #endregion
    }
}
